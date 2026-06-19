using Microsoft.AspNetCore.Mvc;
using MiniLibrary.Web.Repositories;
using MiniLibrary.Web.Services;

namespace MiniLibrary.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookService bookService, IBookRepository bookRepository)
        {
            _bookService = bookService;
            _bookRepository = bookRepository; 
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Filter(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var filteredBooks = await _bookService.FilterBooksAsync(categoryId, minPrice, maxPrice);
            return View(filteredBooks);
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _bookService.GetCategoriesWithBooksAsync();
            return View(categories);
        }

        public async Task<IActionResult> DataHealth()
        {
            var books = await _bookService.GetBooksAsync();

            ViewBag.TotalBooks = books.Count; 
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id); 
            if (book == null) return NotFound($"Book entry with ID = {id} cannot be found.");
            
            return View(book);
        }

        public async Task<IActionResult> Stats()
        {
            var books = await _bookService.GetBooksAsync(); 

            ViewBag.TotalTitles = books.Count;
            ViewBag.TotalCopies = books.Sum(b => b.AvailableCopies);
            ViewBag.TotalAssetValue = books.Sum(b => b.RentalPrice * b.AvailableCopies);

            ViewBag.OutOfStockCount = books.Count(b => b.AvailableCopies <= 0);
            ViewBag.LowStockCount = books.Count(b => b.StockStatus == "Low Stock"); 

            return View(books);
        }
    }
}