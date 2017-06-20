using System.Linq;

namespace BusinessEntities.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BusinessEntities.SmartMarketDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BusinessEntities.SmartMarketDB context)
        {
            //create roles
            context.Roles.AddOrUpdate(u => u.Roles,
                new Role
                {
                    Roles = "Admin",
                    IsSystem = true
                },
                new Role
                {
                    Roles = "Company",
                    IsSystem = true
                },
                new Role
                {
                    Roles = "Customer",
                    IsSystem = true
                }
            );
            //create admin user
            context.Users.AddOrUpdate(u => u.UserName, new User
            {
                Active = true,
                Password = "RG9uJ3RMb2dpbg==-bzwUvvK6shM=", //Don'tLogin
                UserName = "Admin",
                IsSystem = true
            },
             new User
             {
                 Active = true,
                 Password = "RG9uJ3RMb2dpbg==-bzwUvvK6shM=", //Don'tLogin
                 UserName = "Samsung",
                 IsSystem = true,
                 UserType = UserType.Company,
                 Email = "test@test.com",
                 Address = "nowhere",
                 Phone = "012345678"
             });

            context.SaveChanges();

            var adminRole = (from r in context.Roles
                             where r.Roles == "Admin"
                             select r).SingleOrDefault();
            var companyRole = (from r in context.Roles
                               where r.Roles == "Company"
                               select r).SingleOrDefault();

            var adminUser = (from r in context.Users
                             where r.UserName == "Admin"
                             select r).SingleOrDefault();
            var samsungUser = (from r in context.Users
                               where r.UserName == "Samsung"
                               select r).SingleOrDefault();
            context.UserRoles.AddOrUpdate(r => new { r.RoleId, r.UserId }, new UserRole
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            }
            , new UserRole
            {
                RoleId = companyRole.Id,
                UserId = samsungUser.Id
            });
            context.SaveChanges();

            context.Companies.AddOrUpdate(c => c.CompanyName,
            new Company()
            {
                CompanyName = "Samsung",
                UserId = samsungUser.Id
            });

            context.Categories.AddOrUpdate(p => p.CategoryName,
                new Category()
                {
                    CategoryName = "Electronics",
                    Description = "Electronics"
                },
                new Category()
                {
                    CategoryName = "Computers",
                    Description = "Computers"
                },
                new Category()
                {
                    CategoryName = "Cell Phones & Accessories",
                    Description = "Cell Phones & Accessories"
                }
                );
            context.SaveChanges();


            var electronicsCategory = context.Categories.SingleOrDefault(c => c.CategoryName == "Electronics");
            if (electronicsCategory != null)
            {
                context.SubCategories.AddOrUpdate(p => new { p.SubCategoryName, p.CategoryId },
                    new SubCategory()
                    {
                        SubCategoryName = "Smart TV",
                        Description = "Smart TV",
                        CategoryId = electronicsCategory.Id
                    });
                context.SaveChanges();
                var smartTVSubCat = context.SubCategories.SingleOrDefault(c => c.SubCategoryName == "Smart TV");
            }
        }
    }
}
