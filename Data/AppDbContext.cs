using Microsoft.EntityFrameworkCore;
using MiniLibrary.Web.Models;

namespace MiniLibrary.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();
        public DbSet<BorrowTicket> BorrowTickets => Set<BorrowTicket>();
        public DbSet<BorrowDetail> BorrowDetails => Set<BorrowDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.BookCode).IsRequired().HasMaxLength(20);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.RentalPrice).HasColumnType("decimal(18,2)");
                
                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Books)
                      .HasForeignKey(b => b.CategoryId);
            });

            modelBuilder.Entity<BorrowDetail>().Property(d => d.DepositPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BorrowTicket>().Property(t => t.TotalDeposit).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Applied Mathematics" },
                new Category { Id = 2, Name = "Computer Science" }
            );

            modelBuilder.Entity<Borrower>().HasData(
                new Borrower { Id = 1, Name = "Nguyen Van A", Email = "studentA@vnu-hcm.edu.vn" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, BookCode = "MATH-001", Title = "Advanced Linear Algebra", Author = "Gilbert Strang", RentalPrice = 15000, AvailableCopies = 10, CategoryId = 1 },
                new Book { Id = 2, BookCode = "MATH-002", Title = "Applied Probability", Author = "Sheldon Ross", RentalPrice = 12000, AvailableCopies = 2, CategoryId = 1 },
                new Book { Id = 3, BookCode = "CS-001", Title = "Deep Learning Overview", Author = "Ian Goodfellow", RentalPrice = 25000, AvailableCopies = 5, CategoryId = 2 },
                new Book { Id = 4, BookCode = "MATH-003", Title = "Discrete Mathematics", Author = "Kenneth Rosen", RentalPrice = 18000, AvailableCopies = 25, CategoryId = 1 },
                new Book { Id = 5, BookCode = "CS-002", Title = "Design Patterns", Author = "Erich Gamma", RentalPrice = 30000, AvailableCopies = 0, CategoryId = 2 }, 
                new Book { Id = 6, BookCode = "CS-003", Title = "Introduction to Algorithms", Author = "Thomas Cormen", RentalPrice = 35000, AvailableCopies = 3, CategoryId = 2 }
            );
        }
    }
}