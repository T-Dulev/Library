namespace LibraryJulesVerne.DTOs
{
    public class UnreturnedBookDto
    {
        public int loan_id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int ReaderId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EGN { get; set; }
        public string? Email { get; set; }
        public DateOnly BorrowedDate { get; set; }
    }
}