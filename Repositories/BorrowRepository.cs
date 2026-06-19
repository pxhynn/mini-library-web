using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniLibrary.Web.Data;
using MiniLibrary.Web.Models;

namespace MiniLibrary.Web.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly AppDbContext _context;
        public BorrowRepository(AppDbContext context) => _context = context;

        public async Task CreateTicketAsync(BorrowTicket ticket) => 
            await _context.BorrowTickets.AddAsync(ticket);

        public Task<List<BorrowTicket>> GetHistoryReadOnlyAsync() =>
            _context.BorrowTickets.Include(t => t.Borrower)
                                  .Include(t => t.BorrowDetails).ThenInclude(d => d.Book)
                                  .AsNoTracking() 
                                  .ToListAsync();
    }
}