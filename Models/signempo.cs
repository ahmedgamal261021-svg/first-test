using System.ComponentModel.DataAnnotations;

namespace first_test.Models
{
    public class signempo

    {
        [Required]
        
        [Display(Name = "email")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
    }

}
