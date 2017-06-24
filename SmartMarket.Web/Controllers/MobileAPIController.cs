using BusinessEntities;
using SmartMarket.Web.Business;
using SmartMarket.Web.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SmartMarket.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MobileApiController : ApiController
    {
        private SmartMarketDB db = new SmartMarketDB();


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

        [HttpGet]
        [Route("api/MobileAPI/GetProducts", Name = "GetProducts")]
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
        public IHttpActionResult GetProduct(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        public IQueryable<SubCategory> GetSubCategories(Guid categoryId)
        {
            return db.SubCategories.Where(c => c.CategoryId == categoryId).OrderBy(c => c.SubCategoryName);
        }

        [AllowAnonymous]
        [HttpGet]
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