
using KinderKulturServer.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KinderKulturServer.Data
{
    public class MariaDbContext : IdentityDbContext<AppUser>
    {
        public MariaDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
