using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace API.Models.Scores
{
    public class ScoresDbContext : DbContext
    {
        public ScoresDbContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            //base.OnModelCreating(modelBuilder);
        }
    }
}