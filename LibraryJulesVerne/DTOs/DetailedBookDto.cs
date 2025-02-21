using LibraryJulesVerne.Models;

namespace LibraryJulesVerne.DTOs
{
    public class DetailedBookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Isbn { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int AvailableCount { get; set; }
        public List<BookLoanDto>? LastFiveLoans { get; set; }
    }

    public class BookLoanDto
    {
        public int LoanId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string? BorrowerName { get; set; }
    }
}