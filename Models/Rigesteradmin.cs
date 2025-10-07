using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel;
namespace first_test.Models
{
    public class Rigesteradmin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [Required]
        [Column("AdminName")]
        [MaxLength(50)]
        public string AdminName { get; set; }

        [Required]
        [Column("phone_num")]
        [MaxLength(11)]
        [Phone]
        public string PhoneNum { get; set; }

        [Required]
        [Column("email")]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        // Optional: Password validation rule
        // [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
        // ErrorMessage = "Password must be at least 8 characters long and contain both letters and numbers.")]
        public string Password { get; set; }

    }
}
