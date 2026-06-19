using System.Collections.Generic;
using System.Threading.Tasks;
using MiniLibrary.Web.Models;

namespace MiniLibrary.Web.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllReadOnlyAsync();
        Task<List<Book>> FilterAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIdReadOnlyAsync(int id);
        Task SaveChangesAsync();

        Task<List<Category>> GetCategoriesWithBooksAsync();
    }
}