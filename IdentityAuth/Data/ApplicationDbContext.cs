using IdentityAuth.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuth.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole,Guid,ApplicationUserClaim,
        ApplicationUserRole,ApplicationUserLogin,ApplicationRoleClaim,ApplicationUserToken>(options)
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
