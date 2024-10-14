using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Egn { get; set; }
        public string Email { get; set; }

        public Reader()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Egn = "";
            Email = "";
        }
    }
}