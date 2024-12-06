// LibraryContext.cs
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../../Library.db");
    }
}