using Microsoft.EntityFrameworkCore;

namespace LibraryJulesVerne
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        // Define the DbSet properties here
    }
}