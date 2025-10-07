using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace first_test.Models
{
    public class JobAddViewModel
    {
        public List<Jop> Jobs { get; set; } =new List<Jop> ();
        public Jop NewJob { get; set; } = new Jop();
        public List<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();
    }
    public class JobWithCompanyViewModel
    {
        public Jop Job { get; set; }
        public userwithjob usjo { get; set; }
        public Rigesteruser User { get; set; }
        public Rigesterempo Company { get; set; }
    }
}
