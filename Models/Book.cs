namespace Library.Models

{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int AvailableCount { get; set; }

        public Book()
        {
            Id = 0;
            Title = "";
            Isbn = "";
            Author = "";
            Genre = "";
            AvailableCount = 0;
        }
    }
}