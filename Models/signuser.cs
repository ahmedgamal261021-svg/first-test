using System.ComponentModel.DataAnnotations;

namespace first_test.Models
{
    public class signuser

    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
    }

}
