
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first_test.Models
{
    [Table("Company")]
    public class Rigesterempo
    {
        [Key]
        
        public int Company_id { get; set; }
        [Required]
        [ MaxLength(100)]
        [Column("emp_fname")]
        public  string emp_fname { get; set; }
        [Required, MaxLength(100)]
        [Column("emp_lname")]
        public string emp_lname { get; set; }
        [Required, MaxLength(15)]
        [Column("emp_phone")]
        public string emp_phone{ get; set; }
        [Required, MaxLength(100)]
        [DataType(DataType.Password)]
        [Column("password")]
        public string Password { get; set; } = null!;
        
        [MaxLength(100)]
        [Column("companyName")]
        public string companyName { get; set; }
        [Required, MaxLength(100)]
        [EmailAddress]
        [Column("emp_email")]
        public string emp_email { get; set; }
        
        [Required, MaxLength(100)]
        [Column("job_title")]
        public  string job_title { get; set; }
        [Required, MaxLength(100)]
        [Column("country")]
        public string country { get; set; }

        [Required, MaxLength(100)]
        [Column("government")]
        public string government { get; set; }
        [Required, MaxLength(100)]
        [Column("field_of_work")]
        public string field_of_work { get; set; }
        public ICollection<Jop> jobs { get; set; } = new List<Jop>();

        [Column("profile_photo")]
        [MaxLength(255)]
        public string? ProfilePhoto { get; set; }
        public int? PriceId { get; set; }
        public PricPlan? PricingPlan { get; set; }
    }

    public class ChangePasswordCampony
    {
        [Required]
        public string password { get; set; }
        [Required]

        public string password1 { get; set; }
        [Required]
        [Compare("password1", ErrorMessage = "Passwords do not match.")]
        public string password2 { get; set; }
    }
    public class  InformationCompanys
    {
        public Rigesterempo rigesterempo { get; set; } 
        public int CountofJop { get; set; }
        public int CountofApply{ get; set; }
    }
}
   

