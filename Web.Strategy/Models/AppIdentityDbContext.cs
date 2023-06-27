using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Strategy.Models;

namespace Web.Strategy.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        // options'u Program.cs'te dolduracağız.(ConnectionStrings vs..)
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
