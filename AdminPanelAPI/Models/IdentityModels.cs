using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using AdminPanelAPI.Models.DataModels;

namespace AdminPanelAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("NewsAdminPanelConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<NewsCategoryModel> NewsCategories { get; set; }
        public DbSet<NewsIdentityModel> NewsIdentities { get; set; }
        public DbSet<NewsContentModel> NewsContents { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<StructureSectionModel> StructureSections { get; set; }
        public DbSet<NewsPositionModel> NewsPositions { get; set; }
        public DbSet<NewsImagesModel> NewsImages { get; set; }
    }
}