using first_test.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace first_test.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>option):base(option)
        {

        }
        public DbSet<Rigesteruser> Rigsteruser { get; set;}
        public DbSet<ContactUs> ContactUs { get; set;}
        public DbSet<Rigesteradmin> Rigesteradmin { get; set;}
        public DbSet<Rigesterempo> Rigesterempo { get; set; }
        public DbSet<PricPlan> PricPlan { get; set; }
        public DbSet<CreateCategory> CreateCategory { get; set; }
        public DbSet<Jop> jobadd { get; set; }
        public DbSet<userwithjob> userwithjob { get; set; }
        public DbSet<InterviewInformation> InterviewInformation { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<userwithjob>()
                .HasOne(uj => uj.User)
                .WithMany(u => u.userwithjobs)
                .HasForeignKey(j => j.Userid);
            modelBuilder.Entity<userwithjob>()
                .HasOne(uj => uj.job)
                .WithMany(u => u.userwithjobs)
                .HasForeignKey(j => j.jobid);
            modelBuilder.Entity<Jop>()
                .HasOne(j => j.Company).WithMany(c => c.jobs).HasForeignKey(j => j.CompanyId);

            modelBuilder.Entity<Rigesteruser>()
    .HasOne(u => u.InterviewInformation)
    .WithOne(i => i.User)
    .HasForeignKey<InterviewInformation>(i => i.userid).IsRequired();

            modelBuilder.Entity<Rigesterempo>()
       .HasOne(c => c.PricingPlan)
       .WithMany(p => p.Companies)
       .HasForeignKey(c => c.PriceId)
       .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ContactUs>()
       .HasOne(c => c.Rigesterusera)            // كل ContactUs له User واحد
       .WithMany(u => u.ContactUs)         // واليوزر ممكن يبقى عنده كذا ContactUs
       .HasForeignKey(c => c.UserId)           // مفتاح العلاقة
       .OnDelete(DeleteBehavior.Cascade);
           
            
        modelBuilder.Entity<Jop>().HasOne(j=>j.Category).WithMany(c=>c.Jop).HasForeignKey(j => j.CategoryId);
        }


    }

}
