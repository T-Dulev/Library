using System.ComponentModel.DataAnnotations;

namespace LibraryJulesVerne.Models
{
    public class BookLoan
    {
        [Key]
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public Book? Book { get; set; }
        public Reader? Reader { get; set; }
    }
}