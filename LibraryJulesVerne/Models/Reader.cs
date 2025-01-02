using System.ComponentModel.DataAnnotations;

namespace LibraryJulesVerne.Models
{
    public class Reader
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string Email { get; set; }

        // Навигационно свойство към BookLoans
        public ICollection<BookLoan> BookLoans { get; set; }
    }
}
