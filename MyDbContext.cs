namespace Library
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Xml;

    public class LibraryContext : DbContext
    {
        public DbSet<MyEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("Library.db");
        }
    }
}
