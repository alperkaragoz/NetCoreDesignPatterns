using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        // options'u Program.cs'te dolduracağız.(ConnectionStrings vs..)
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
    }
}
