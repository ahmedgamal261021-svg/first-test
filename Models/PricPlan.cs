using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace first_test.Models
{
    public class PricPlan
    {
        [Key]
        // Match EXACT DB name
        public int PriceId { get; set; }
        [Required]
        [Column("subscriptionName")]
        [MaxLength(100)]
        public string subscriptionname { get; set; }
        [Required]
        [Column("subscriptionPrice", TypeName = "decimal(18,2)")]

        public decimal subscriptionprice { get; set; }

        [Required]
        [Column("SubscriptionType")]
        [MaxLength(100)]
        public string Subscriptiontype { get; set; }

        [Required]
        [Column("DescriptionPricin")]
        [MaxLength(100)]
        public string DescriptionPricin { get; set; }
        public ICollection<Rigesterempo> Companies { get; set; } = new List<Rigesterempo>();


    }
    public class PricePlanViewModel
    {
        public PricPlan NewPlan { get; set; } = new PricPlan();
        public List<PricPlan> AllPlans { get; set; } = new List<PricPlan>();
    }
}
