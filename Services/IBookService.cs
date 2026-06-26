using Microsoft.Extensions.Options;
using MiniLibrary.Web.Models;
using MiniLibrary.Web.Options;
using MiniLibrary.Web.Repositories;
using MiniLibrary.Web.ViewModels;

namespace MiniLibrary.Web.Services
{
    public interface IBookService
    {
        Task<List<BookListItemViewModel>> GetBooksAsync();
        Task<List<BookListItemViewModel>> FilterBooksAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Category>> GetCategoriesWithBooksAsync();
    }
}