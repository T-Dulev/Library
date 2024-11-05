namespace Library.Models
{
    public class Readers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Egn { get; set; }
        public string Email { get; set; }

        public Readers()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Egn = "";
            Email = "";
        }
    }
}