using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first_test.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        [Column("Nama")]
        public string name { get; set; }
        [Required]
        [Column("email")]
        [EmailAddress]

        public string email{ get; set; }
        [Required]
        [Column("Subject")]
        [MaxLength(252)]
        public string Subject{ get; set; }

        [Required]
        [Column("Message")]
        [MaxLength(255)]
        public string Message{ get; set; }
        [Column("interview_date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ContuctUs_date { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public Rigesteruser? Rigesterusera { get; set; }
    }
    public class rigewithcontact
    {
        [ValidateNever]
        public Rigesteruser rigesteruser { get; set; }
        public ContactUs ContactUs { get; set; }
        public List<Jop> jop { get; set; } = new List<Jop>();
        public List<CreateCategory> CreateCategory { get; set; } = new List<CreateCategory>();


    }
    //public class Collection
    //{

    //    public List<CreateCategory> category { get; set; } = new List<CreateCategory>();
    //    public List<Jop> jop { get; set; } = new List<Jop>();
    //    public ContactUs Message { get; set; } 



    //}
}
