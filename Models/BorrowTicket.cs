using System;
using System.Collections.Generic;

namespace MiniLibrary.Web.Models
{
    public class BorrowTicket
    {
        public int Id { get; set; }
        
        public int BorrowerId { get; set; }
        public Borrower? Borrower { get; set; }
        
        public DateTime BorrowedAt { get; set; } = DateTime.Now;
        public decimal TotalDeposit { get; set; }

        public ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();
    }
}