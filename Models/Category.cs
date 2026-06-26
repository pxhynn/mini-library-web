using System.Collections.Generic;

namespace MiniLibrary.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Quan hệ 1-Nhiều: Một thể loại có nhiều sách
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}