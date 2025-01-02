using System.ComponentModel.DataAnnotations;

namespace LibraryJulesVerne.Models
{
    public class BookLoan
    {
        [Key]
        public int loan_id { get; set; }
        public int book_id { get; set; }
        public int reader_id { get; set; }
        public DateTime borrowed_date { get; set; }
        public DateTime? returned_date { get; set; }

        // Навигационни свойства към Book и Reader
        public Book? Book { get; set; }
        public Reader? Reader { get; set; }
    }
}