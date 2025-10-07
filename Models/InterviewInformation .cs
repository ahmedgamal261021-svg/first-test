using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace first_test.Models
 {
    [Table("InterviewInformation")]
     public class InterviewInformation
    {
        [Key]
  
        public int id { get; set; }
        [Required]
       
        public  int userid { get; set; }
       
      
        public Rigesteruser? User { get; set; }
     
        [Column("jobname")]
        [MaxLength(100)]
        public string job_name { get; set; }
        [Required]
        [Column("companyName")]
        [MaxLength(100)]
        public string companyName{ get; set; }
        [Required]
        [Column("interview_date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime interview_date { get; set; }
        [Required]
        [Column("contact_number")]
        public int contact_number { get; set; }
        [StringLength(10, ErrorMessage = "Address cannot exceed 200 characters.")]
        [Display(Name = "Interview Address")]
        public string interview_address { get; set; }
        [Required]
        [Column("interview_result")]
        [MaxLength(100)]
        public string interview_result { get; set; }


    }
    public class userwithstate
    
    {
        //public List<Rigesteruser> rigesteruser { get; set; } = new List<Rigesteruser>();
        //public List<InterviewInformation> States { get; set; } = new List<InterviewInformation>();
        //public List<String> States { get; set; } = new List<String>();
        public Rigesteruser rigesteruser { get; set; }
        public InterviewInformation States { get; set; }



    }
}
