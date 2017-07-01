using BusinessEntities;
using SmartMarket.Web.Business;
using SmartMarket.Web.Helpers;
using SmartMarket.Web.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace SmartMarket.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AutoInvalidateCacheOutput]
    public class MobileApiController : ApiController
    {
        private SmartMarketDB db = new SmartMarketDB();

        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        public IQueryable<Category> GetCategories(Guid? categoryId)
        {
            IQueryable<Category> lstCategories;
            if (categoryId.HasValue)
            {
                lstCategories = (from p in db.Products
                                 where p.SubCategory.CategoryId == categoryId.Value
                                 select p.SubCategory.Category).Distinct();
            }
            else
            {
                lstCategories = (from p in db.Products
                                 select p.SubCategory.Category).Distinct();
            }
            return lstCategories.OrderBy(c => c.CategoryName);
        }
        // [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        public IQueryable<Category> GetCompanyCategories(Guid? companyId)
        {
            IQueryable<Category> lstCategories;

            lstCategories = (from p in db.Products
                             join sc in db.SubCategories
                             on p.SubCategoryId equals sc.Id
                             join c in db.Categories
                             on sc.CategoryId equals c.Id
                             where p.CompanyId == companyId.Value
                             select c).Distinct();


            return lstCategories.OrderBy(c => c.CategoryName);
        }

        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        public IQueryable<Company> GetCompanies(Guid? companyId)
        {
            IQueryable<Company> lstCompanies;
            if (companyId.HasValue)
            {
                lstCompanies = db.Companies.Where(c => c.Id == companyId);
            }
            else
            {
                lstCompanies = db.Companies;
            }
            return lstCompanies.OrderBy(c => c.CompanyName);
        }

        [System.Web.Http.HttpGet]
        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        [System.Web.Http.Route("api/MobileAPI/GetProducts", Name = "GetProducts")]
        public IQueryable<Product> GetProducts(Guid? categoryId, Guid? companyId, string search = "")
        {
            IQueryable<Product> lstProducts;
            if (!string.IsNullOrWhiteSpace(search))
            {
                lstProducts = db.Products.Where(p => p.ProductName.Contains(search));
            }
            else
            {
                lstProducts = db.Products;
            }
            if (categoryId.HasValue)
            {
                lstProducts = lstProducts.Where(p => p.SubCategory.CategoryId == categoryId.Value);
            }
            if (companyId.HasValue)
            {
                lstProducts = lstProducts.Where(p => p.CompanyId == companyId.Value);
            }
            return lstProducts.OrderBy(p => p.ProductName).ThenByDescending(p => p.Rate);
        }

        [ResponseType(typeof(Product))]
        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        public IHttpActionResult GetProduct(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        public IQueryable<SubCategory> GetSubCategories(Guid categoryId)
        {
            return db.SubCategories.Where(c => c.CategoryId == categoryId).OrderBy(c => c.SubCategoryName);
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Login(string userName, string password)
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            SmartMarket.Web.Business.User _user = new SmartMarket.Web.Business.User();
            if (_user.Authenticate(userName, password, IP))
            {
                Roles oRoles = new Roles();
                if (oRoles.IsUserInRole(userName, "Customer"))
                {

                    return Json(new { success = true, Message = "", UserId = _user.GetUserId(userName) });
                }

                return Json(new { success = false, Message = "no a customer", UserId = -1 });
            }

            return Json(new { success = false, Message = "Invalid username or password", UserId = -1 });

        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCustomer(long userId)
        {

            var customer = db.Customers.Where(c => c.UserId == userId).Include(c => User).SingleOrDefault();
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);



        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PlaceOrder([FromBody] OrderModel orderModel)
        {
            try
            {
                using (SmartMarketDB _db = new SmartMarketDB())
                {

                    long userId = orderModel.order.UserId;
                    var singleOrDefault = _db.Customers.Where(c => c.UserId == userId).SingleOrDefault();
                    if (singleOrDefault != null)
                    {
                        var customerId = singleOrDefault.Id;

                        var order = new Order { CustomerId = customerId };
                        order = _db.Orders.Add(order);
                        _db.SaveChanges();
                        foreach (var orderItem in orderModel.order.OrderItems)
                        {
                            orderItem.OrderId = order.Id;
                            orderItem.PricePerItem = _db.Products.SingleOrDefault(p => p.Id == orderItem.ProductId).Price;
                            _db.OrderItems.Add(orderItem);
                        }
                        _db.SaveChanges();

                        return Json(new { success = true, Message = "", OrderId = order.Id });
                    }
                    return Json(new { success = false, Message = "invalid customer", OrderId = -1 });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, Message = ex.Message, OrderId = -1 });
            }

        }

        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.HttpPost]
        public IHttpActionResult Signup([FromBody]BusinessEntities.User user)
        {

            using (SmartMarketDB _db = new SmartMarketDB())
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        string encodedPassword = TextEncoding.EncodeString(user.Password);
                        user.Password = encodedPassword;
                        user = _db.Users.Add(user);
                        _db.SaveChanges();
                        string roleName = "";
                        switch (user.UserType)
                        {
                            case UserType.Company:
                                roleName = "Company";
                                break;
                            case UserType.Customer:
                                roleName = "Customer";
                                break;
                            case UserType.EStore:
                                roleName = "Admin";
                                break;
                        }
                        if (!string.IsNullOrEmpty(roleName))
                        {
                            var role = _db.Roles.SingleOrDefault(r => r.Roles == roleName);
                            var userRole = new UserRole
                            {
                                Role = role,
                                User = user
                            };
                            _db.UserRoles.Add(userRole);
                        }
                        _db.SaveChanges();
                        if (user.UserType == UserType.Customer)
                        {
                            var customer = new Customer
                            {
                                User = user,
                                Gender = Gender.Male,
                                Nikename = user.UserName
                            };
                            _db.Customers.Add(customer);
                            _db.SaveChanges();
                        }
                        dbContextTransaction.Commit();

                        Business.User _user = new Business.User();
                        return Json(new { success = true, Message = "", UserId = _user.GetUserId(user.UserName) });
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return Json(new { success = false, Message = ex.Message + ex.StackTrace, UserId = -1 });
                    }
                }

            }
        }

        public IQueryable<Order> GetOrdersByUserId(long userId)
        {
            IQueryable<Order> lstOrders;

            lstOrders = (from o in db.Orders
                         where o.Customer.User.Id == userId
                         select o).Include(o => o.OrderItems);


            return lstOrders.OrderByDescending(c => c.OrderDate);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}