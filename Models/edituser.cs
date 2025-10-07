using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace first_test.Models
{
    public class edituser
    {
        public int id  { get; set; }
        public string fname { get; set; }

        public string lname { get; set; }

        public string lnaphoneme { get; set; } // Consider renaming to phone

        public int age { get; set; }


        public string? ProfilePhoto { get; set; }

        public string countrySelect { get; set; }

        public string governante { get; set; }

        public string education_level { get; set; }

        public string experience_level { get; set; }
    }
    public class editcompany
    {
        public int id { get; set; }
        public string fname { get; set; }

        public string lname { get; set; }

        public string lnaphoneme { get; set; } // Consider renaming to phone
        public string companyName { get; set; } // Consider renaming to phone

        public int age { get; set; }
        [EmailAddress]
        
        public string email { get; set; }
     

        public string job_title { get; set; }
        public string field_of_work { get; set; }
    }
}
