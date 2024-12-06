using Microsoft.EntityFrameworkCore;
using LibraryJulesVerne.Models;

namespace LibraryJulesVerne.Context
{
    public class LibraryJulesVerneContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data source=../../Library.db");
    }
}