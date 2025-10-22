using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<GatePass> GatePasses { get; set; }
        public DbSet<LockerRequest> LockerRequests { get; set; }
    }
}