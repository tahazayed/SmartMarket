using BusinessEntities;
using System.Linq;
using System.Web.Http;

namespace SmartMarket.Web.Controllers
{
    public class MobileAPIController : ApiController
    {
        private SmartMarketDB db = new SmartMarketDB();

        // GET: api/CategoriesAPI
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
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