using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace first_test.Models
{
    [Table("User")]
    public class Rigesteruser
    {

            [Key]
            [Column("user_id")]
            public int UserId { get; set; }

            [Column("name")]
            [MaxLength(100)]
        [Required]
            public string? Name { get; set; }

        [Column("phone_num")]
        [StringLength(20)]
        [Required]
        [Phone]                                     // basic format check
        [Display(Name = "Phone number")]
        public string? PhoneNum { get; set; }

            [Column("age")]
            public int? Age { get; set; }
        [Required]
        [Column("gender")]

        public Gender Gender { get; set; } = Gender.Other;


        [Column("password")]
            [MaxLength(255)]
            [Required]
            public string? Password { get; set; }

            /*[Column("profile_photo")]
            [MaxLength(255)]
            public string? ProfilePhoto { get; set; }*/

            [Column("job_name")]
            [MaxLength(100)]
      
        public string? JobName { get; set; }
        [Required]
        [Column("country")]
            [MaxLength(100)]
        
        public string? Country { get; set; }

            [Column("government")]
            [MaxLength(100)]
        [Required]
        public string? Government { get; set; }

            [Column("education_level")]
            [MaxLength(100)]
        [Required]
        public string? EducationLevel { get; set; }

            [Column("experience_level")]
            [MaxLength(100)]
        [Required]
        public string? ExperienceLevel { get; set; }
        [Column("profile_photo")]
        [MaxLength(255)]
        public string? ProfilePhoto { get; set; }

        public ICollection<userwithjob> userwithjobs { get; set; } = new List<userwithjob>();
        public InterviewInformation? InterviewInformation { get; set; }
        public ICollection<ContactUs> ContactUs { get; set; } = new List<ContactUs>();



    }
    public enum Gender
    {
        Male=0,
        Female=1,
        Other=2
    }
    public class ShowApplicantsViewModel
    {
         public List<Rigesteruser> applicant { get; set;} 
    }
    public class ChangePassword
    {
        [Required]
        public string password{ get; set; }
        [Required]
       
        public string password1{ get; set; }
        [Required]
        [Compare("password1", ErrorMessage = "Passwords do not match.")]
        public string password2{ get; set; }
    }
}
