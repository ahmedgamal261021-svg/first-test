using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace first_test.Models
{
    [Table("job")]
    public class Jop
    {
        [Key]
        [Column("job_id")]
        public int job_id { get; set; }
        [Required]
        [Column("job_name")]
        [MaxLength(100)]
        public string? job_name { get; set; }
        [Required]
        [Column("job_field")]
        [MaxLength(100)]
        public string? job_field { get; set; }
        [Required]
        [Column("country")]
        [MaxLength(100)]
        public string? country { get; set; }
        [Required]
        [Column("government")]
        [MaxLength(100)]
        public string? government { get; set; }

        [Required]
        [Range(22, 60, ErrorMessage = "Age must be less than 60.")]
        [Column("age")]

        public int age { get; set; }
        [Required]
        [Column("type_of_job")]
        [MaxLength(100)]
        public string?type_of_job { get; set; }
        [Required]
        [Column("number_of_employees")]
        public int number_of_employees { get; set; }
        [Required]
        [Column("job_details")]
        [MaxLength(100)]
        public string? job_details { get; set; }
        [Required]
        [Column("educational_level")]
        [MaxLength(100)]
        public string? educational_level { get; set; }
        [Required]
        [Column("years_of_experience")]
        [MaxLength(100)]
        public string? years_of_experience { get; set; }
        [Required]
        [Column("Gender")]
        public Gender Gender { get; set; } = Gender.Other;
        [Column("salary_min")]
        public int MinimumSalary { get; set; }

        [Column("salary_max")]
        public int MaximumSalary { get; set; }
        public ICollection<userwithjob> userwithjobs { get; set; } = new List<userwithjob>();
        
        public int CompanyId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CompanyId))]
        public Rigesterempo Company { get; set; } = null!;
        public int CategoryId{ get; set; }
        [ForeignKey("CategoryId")]
        public CreateCategory? Category { get; set; }



    }
    [Table("userwithjob")]
    public class userwithjob
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Rigesteruser")]
        public int Userid  { get; set; }
        public Rigesteruser? User { get; set; } 
        [Required]
        [Column("job_name")]
        [MaxLength(100)]

        public string job_name { get; set; }
        [Required]
        [Column("CompanyName")]
        [MaxLength(100)]
        public string company { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        
        public string request_state { get; set; }

        [Required]
        [ForeignKey("jobadd")]
        public int jobid { get; set; } 
        public Jop? job { get; set ; } 
    }


}
    
