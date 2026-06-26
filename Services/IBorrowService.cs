using MiniLibrary.Web.Data;
using MiniLibrary.Web.Models;
using MiniLibrary.Web.Repositories;
using MiniLibrary.Web.ViewModels;

namespace MiniLibrary.Web.Services
{
    public interface IBorrowService
    {
        Task ProcessBorrowingAsync(BorrowCreateViewModel model);
    }
}