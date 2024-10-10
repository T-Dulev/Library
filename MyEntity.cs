using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class MyEntity
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string? Name { get; set; }
    }
}
