using MiniLibrary.Web.Data;
using MiniLibrary.Web.Models;
using MiniLibrary.Web.Repositories;
using MiniLibrary.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace MiniLibrary.Web.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowRepository _borrowRepository;

        public BorrowService(AppDbContext context, IBookRepository bookRepository, IBorrowRepository borrowRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _borrowRepository = borrowRepository;
        }

        public async Task ProcessBorrowingAsync(BorrowCreateViewModel model)
        {
            if (!model.BookId.HasValue || !model.Quantity.HasValue)
            {
                throw new Exception("Book Selection and Borrow Quantity fields are required.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var book = await _bookRepository.GetByIdAsync(model.BookId.Value);
                if (book == null) throw new Exception("Book item could not be found.");
                
                if (book.AvailableCopies < model.Quantity.Value) 
                    throw new Exception($"Insufficient copies. Only {book.AvailableCopies} copies available for '{book.Title}'.");

                book.AvailableCopies -= model.Quantity.Value;

                var ticket = new BorrowTicket
                {
                    BorrowerId = 1, 
                    BorrowedAt = DateTime.Now,
                    TotalDeposit = book.RentalPrice * model.Quantity.Value * 2
                };
                
                await _borrowRepository.CreateTicketAsync(ticket);
                await _bookRepository.SaveChangesAsync();

                var detail = new BorrowDetail
                {
                    BorrowTicketId = ticket.Id,
                    BookId = book.Id,
                    Quantity = model.Quantity.Value,
                    DepositPrice = book.RentalPrice * 2
                };
                ticket.BorrowDetails.Add(detail);
                
                await _bookRepository.SaveChangesAsync();
                await transaction.CommitAsync(); 
            }
            catch
            {
                await transaction.RollbackAsync(); 
                throw;
            }
        }
    }
}