namespace MiniLibrary.Web.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal RentalPrice { get; set; }
        public int AvailableCopies { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; } 
    }
}