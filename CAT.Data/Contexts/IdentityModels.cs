using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using CAT.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CAT.Contexts.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Showcase> Showcases { get; set; }
        public DbSet<MomentShowcase> MomentsShowcases { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}