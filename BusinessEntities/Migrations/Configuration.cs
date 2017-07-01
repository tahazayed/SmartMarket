using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;

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
                IsSystem = true,
                Email = "test@test.com",
                UserType = UserType.EStore
            },
             new User
             {
                 Active = true,
                 Password = "RG9uJ3RMb2dpbg==-bzwUvvK6shM=", //Don'tLogin
                 UserName = "Samsung",
                 IsSystem = false,
                 UserType = UserType.Company,
                 Email = "test@test.com",
                 Address = "nowhere",
                 Phone = "012345678"
             },
             new User
             {
                 Active = true,
                 Password = "RG9uJ3RMb2dpbg==-bzwUvvK6shM=", //Don'tLogin
                 UserName = "metro",
                 IsSystem = false,
                 UserType = UserType.Company,
                 Email = "test@test.com",
                 Address = "nowhere",
                 Phone = "012345678"
             });

            SaveChanges(context);

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
            var metroUser = (from r in context.Users
                             where r.UserName == "metro"
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
            }, new UserRole
            {
                RoleId = companyRole.Id,
                UserId = metroUser.Id
            });
            context.SaveChanges();

            context.Companies.AddOrUpdate(c => c.CompanyName,
            new Company()
            {
                CompanyName = "Samsung",
                UserId = samsungUser.Id,
                ImageURL = "http://s7d2.scene7.com/is/image/SamsungUS/samsung-logo-191-1"
            },
             new Company()
             {
                 CompanyName = "Metro",
                 UserId = metroUser.Id,
                 ImageURL = "http://www.mansourgroup.com/Cms_Data/Sites/Mansour/Themes/assets/img/Metro.jpg"
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
                var samsungCompany = context.Companies.SingleOrDefault(c => c.CompanyName == "Samsung");
                context.Products.AddOrUpdate(p => new { p.ProductName, p.CompanyId },
                new Product()
                {
                    ProductName = "Samsung Electronics QN55Q7C Curved 55-Inch 4K Ultra HD Smart QLED TV (2017 Model)",
                    Description = "Samsung Electronics QN55Q7C Curved 55-Inch 4K Ultra HD Smart QLED TV (2017 Model)",
                    CompanyId = samsungCompany.Id,
                    SubCategoryId = smartTVSubCat.Id,
                    Price = 44946,
                    ImageURL = "https://images-na.ssl-images-amazon.com/images/I/41D2IDqQ51L._AC_US327_FMwebp_QL65_.jpg",
                    Rate = 4.5,
                    AvailableStock = 500

                });
                SaveChanges(context);
            }

            var smpleProductsString = ReadFileContent("simpleProducts.json");
            var ldtSimpleProducts = JsonConvert.DeserializeObject<List<SimpleProduct>>(smpleProductsString);
            var metroCompany = context.Companies.SingleOrDefault(c => c.CompanyName == "metro");

            foreach (var simpleProduct in ldtSimpleProducts)
            {
                context.Categories.AddOrUpdate(p => p.CategoryName,
                    new Category()
                    {
                        CategoryName = simpleProduct.CategoryName,
                        Description = simpleProduct.CategoryName
                    });
                SaveChanges(context);
                var category = context.Categories.SingleOrDefault(c => c.CategoryName == simpleProduct.CategoryName);
                context.SubCategories.AddOrUpdate(p => new { p.SubCategoryName, p.CategoryId },
                    new SubCategory()
                    {
                        SubCategoryName = simpleProduct.SubCategoryName,
                        Description = simpleProduct.SubCategoryName,
                        CategoryId = category.Id
                    });
                context.SaveChanges();
                var subCat = context.SubCategories.SingleOrDefault(c => c.SubCategoryName == simpleProduct.SubCategoryName);
                context.Products.AddOrUpdate(p => new { p.ProductName, p.CompanyId },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = simpleProduct.ProductName,
                    Description = simpleProduct.Description,
                    CompanyId = metroCompany.Id,
                    SubCategoryId = subCat.Id,
                    Price = simpleProduct.Price,
                    ImageURL = simpleProduct.ImageURL,
                    Rate = 0,
                    AvailableStock = 500

                });
                SaveChanges(context);

            }

        }
        private void SaveChanges(SmartMarketDB context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new Exception(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                    ); // Add the original exception as the innerException
            }
        }
        private static string ReadFileContent(string fileName)
        {
            var projpath = new Uri(Path.Combine(new[] { AppDomain.CurrentDomain.BaseDirectory, "..\\.." })).AbsolutePath;
            var projPathString = Uri.UnescapeDataString(projpath);
            using (var reader = new StreamReader(projPathString + "\\Data\\" + fileName))
            {
                return reader.ReadToEnd();
                ;
            }
        }
    }
}
