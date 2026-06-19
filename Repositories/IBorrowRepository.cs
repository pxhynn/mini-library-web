using System.Collections.Generic;
using System.Threading.Tasks;
using MiniLibrary.Web.Models;

namespace MiniLibrary.Web.Repositories
{
    public interface IBorrowRepository
    {
        Task CreateTicketAsync(BorrowTicket ticket);
        Task<List<BorrowTicket>> GetHistoryReadOnlyAsync();
    }
}