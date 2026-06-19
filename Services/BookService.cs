using Microsoft.Extensions.Options;
using MiniLibrary.Web.Models;
using MiniLibrary.Web.Options;
using MiniLibrary.Web.Repositories;
using MiniLibrary.Web.ViewModels;

namespace MiniLibrary.Web.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly LibrarySettings _settings;

        public BookService(IBookRepository bookRepository, IOptions<LibrarySettings> options)
        {
            _bookRepository = bookRepository;
            _settings = options.Value; 
        }

        public async Task<List<BookListItemViewModel>> GetBooksAsync()
        {
            var books = await _bookRepository.GetAllReadOnlyAsync();
            return MapToViewModel(books);
        }

        public async Task<List<BookListItemViewModel>> FilterBooksAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var books = await _bookRepository.FilterAsync(categoryId, minPrice, maxPrice);
            return MapToViewModel(books);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id); 
        }

        public async Task<List<Category>> GetCategoriesWithBooksAsync()
        {
            return await _bookRepository.GetCategoriesWithBooksAsync();
        }

        private List<BookListItemViewModel> MapToViewModel(List<Book> books)
        {
            return books.Select(b => new BookListItemViewModel
            {
                Id = b.Id,
                BookCode = b.BookCode,
                Title = b.Title,
                Author = b.Author,
                CategoryName = b.Category?.Name ?? "N/A",
                RentalPrice = b.RentalPrice,
                AvailableCopies = b.AvailableCopies,

                StockStatus = b.AvailableCopies <= 0 ? "Out of Stock" : 
                              b.AvailableCopies <= _settings.LowAvailableCopyThreshold ? "Low Stock" : "Available",
                BadgeClass = b.AvailableCopies <= 0 ? "badge-danger" : 
                             b.AvailableCopies <= _settings.LowAvailableCopyThreshold ? "badge-warning" : "badge-success"
            }).ToList();
        }
    }
}