using System.ComponentModel.DataAnnotations;

namespace LibraryJulesVerne.Models
{
    public class BookTaken
    {
        public class Book
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Isbn { get; set; }
            public string? Author { get; set; }

            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? EGN { get; set; }
            public string? Email { get; set; }

            public DateTime? borrowed_date { get; set; }

        }
    }
}
