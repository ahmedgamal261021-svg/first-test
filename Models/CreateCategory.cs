using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first_test.Models
{
    public class CreateCategory
    {
        [Key]

        public int CategoryId { get; set; }
        [Column("CategoryName")]
        [Required]
        public string CategoryName { get; set; }


        public ICollection<Jop> Jop { get; set; }
    }
}
