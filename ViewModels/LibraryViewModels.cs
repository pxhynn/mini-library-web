using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.Web.ViewModels
{
    public class BookListItemViewModel
    {
        public int Id { get; set; }
        public string BookCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal RentalPrice { get; set; }
        public int AvailableCopies { get; set; }
        public string StockStatus { get; set; } = string.Empty;
        public string BadgeClass { get; set; } = string.Empty;
    }

    public class BorrowCreateViewModel
    {
        [Required(ErrorMessage = "Borrower Name is required.")]
        [Display(Name = "Borrower Full Name")]
        public string BorrowerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Borrower Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [Display(Name = "Borrower Email Address")]
        public string BorrowerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a target book.")]
        [Display(Name = "Select Target Book")]
        public int? BookId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1 unit.")]
        [Display(Name = "Quantity to Borrow")]
        public int? Quantity { get; set; }

        public List<SelectListItem> BookOptions { get; set; } = new List<SelectListItem>();
    }
}