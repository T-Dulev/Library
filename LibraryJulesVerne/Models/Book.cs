using System.ComponentModel.DataAnnotations;

namespace LibraryJulesVerne.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Isbn { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public string? Genre { get; set; }

        public int AvailableCount { get; set; }

        // Навигационно свойство към BookLoans
        public ICollection<BookLoan>? BookLoans { get; set; }
    }
}