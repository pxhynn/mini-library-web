using Microsoft.EntityFrameworkCore;
using MiniLibrary.Web.Data;
using MiniLibrary.Web.Models;

namespace MiniLibrary.Web.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context) => _context = context;

        public Task<List<Book>> GetAllReadOnlyAsync() =>
            _context.Books.Include(b => b.Category).AsNoTracking().ToListAsync(); 

        public Task<List<Book>> FilterAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Books.Include(b => b.Category).AsNoTracking(); 
            if (categoryId.HasValue) query = query.Where(b => b.CategoryId == categoryId);
            if (minPrice.HasValue) query = query.Where(b => b.RentalPrice >= minPrice);
            if (maxPrice.HasValue) query = query.Where(b => b.RentalPrice <= maxPrice);
            return query.ToListAsync();
        }

        public Task<Book?> GetByIdAsync(int id) =>
            _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
        
        public Task<List<Category>> GetCategoriesWithBooksAsync()
        {
            return _context.Categories
                        .Include(c => c.Books)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();

        public Task<Book?> GetByIdReadOnlyAsync(int id) =>
            _context.Books.Include(b => b.Category).AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }
}