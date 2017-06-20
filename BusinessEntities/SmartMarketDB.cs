using System.Data.Entity;

namespace BusinessEntities
{
    public class SmartMarketDB : DbContext
    {
        public SmartMarketDB()
            : base("SmartMarketDB")
        {


        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
