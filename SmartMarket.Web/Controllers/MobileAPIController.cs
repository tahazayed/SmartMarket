using BusinessEntities;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace SmartMarket.Web.Controllers
{
    public class MobileAPIController : ApiController
    {
        private SmartMarketDB db = new SmartMarketDB();

        // GET: api/CategoriesAPI
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
        // GET: api/Products1/5
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