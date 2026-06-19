using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniLibrary.Web.Repositories;
using MiniLibrary.Web.Services;
using MiniLibrary.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLibrary.Web.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository; // Tiêm thêm repo để đọc danh sách sách

        public BorrowController(IBorrowService borrowService, IBorrowRepository borrowRepository, IBookRepository bookRepository)
        {
            _borrowService = borrowService;
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BorrowCreateViewModel
            {
                Quantity = 1,
                BookOptions = await PopulateBookOptionsAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BookOptions = await PopulateBookOptionsAsync();
                return View(model);
            }
            try
            {
                await _borrowService.ProcessBorrowingAsync(model);
                TempData["SuccessMessage"] = "Book borrowing transaction processed successfully!";
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.BookOptions = await PopulateBookOptionsAsync();
                return View(model);
            }
        }

        public async Task<IActionResult> History()
        {
            var history = await _borrowRepository.GetHistoryReadOnlyAsync();
            return View(history);
        }

        private async Task<List<SelectListItem>> PopulateBookOptionsAsync()
        {
            var books = await _bookRepository.GetAllReadOnlyAsync();
            return books.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = $"{b.Title} (Code: {b.BookCode} | Stock: {b.AvailableCopies} units)"
            }).ToList();
        }
    }
}