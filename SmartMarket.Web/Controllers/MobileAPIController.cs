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
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
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
                lstCategories = db.Categories.Where(c => c.Id == categoryId);
            }
            else
            {
                lstCategories = db.Categories;
            }
            return lstCategories.OrderBy(c => c.CategoryName);
        }
        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]
        public IQueryable<Category> GetCompanyCategories(Guid companyId)
        {
            IQueryable<Category> lstCategories;

            lstCategories = (from p in db.Products
                             join sc in db.SubCategories
                             on p.SubCategoryId equals sc.Id
                             join c in db.Categories
                             on sc.CategoryId equals c.Id
                             where p.CompanyId == companyId
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
        [System.Web.Http.HttpPost]
        public IHttpActionResult PlaceOrder([FromBody] OrderModel orderModel)
        {
            try
            {
                using (SmartMarketDB _db = new SmartMarketDB())
                {
 
                    long userId = orderModel.UserId;
                    var singleOrDefault = _db.Customers.Where(c => c.UserId == userId).SingleOrDefault();
                    if (singleOrDefault != null)
                    {
                        var customerId = singleOrDefault.Id;

                        var order = new Order { CustomerId = customerId };
                        order = _db.Orders.Add(order);
                        _db.SaveChanges();
                        foreach (var orderItem in orderModel.OrderItems)
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
        public IHttpActionResult Singup(BusinessEntities.User user)
        {
            try
            {
                using (SmartMarketDB _db = new SmartMarketDB())
                {
                    string encodedPassword = TextEncoding.EncodeString(user.Password);
                    user.Password = encodedPassword;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    Business.User _user = new Business.User();
                    return Json(new { success = true, Message = "", UserId = _user.GetUserId(user.UserName) });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, Message = ex.Message, UserId = -1 });
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