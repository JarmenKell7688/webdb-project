using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for forms
        public DbSet<Gatepass> Gatepasses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure table per hierarchy (TPH) for user inheritance
            builder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student")
                .HasValue<Faculty>("Faculty")
                .HasValue<ApplicationUser>("Admin");

            // Configure relationships
            builder.Entity<Gatepass>()
                .HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}