namespace LibraryJulesVerne.Models.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Isbn { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int AvailableCount { get; set; } // Брой налични копия
    }
}