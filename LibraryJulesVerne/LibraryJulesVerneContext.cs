using Microsoft.EntityFrameworkCore;
using LibraryJulesVerne.Models;

namespace LibraryJulesVerne.Context
{
    public class LibraryJulesVerneContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data source=../Library.db");
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(bl => bl.book_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Reader)
                .WithMany(r => r.BookLoans)
                .HasForeignKey(bl => bl.reader_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.BookLoans)
                .WithOne(bl => bl.Book)
                .HasForeignKey(bl => bl.book_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}