using BusinessEntities;
using SmartMarket.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SmartMarket.Web.Controllers
{

    public class HomeController : Controller
    {
        private SmartMarketDB db = new SmartMarketDB();

        public ActionResult Index()
        {
            var homeModel = new HomeModel();

            var lstCategories = new List<SelectListItem>
            {
                new SelectListItem {Selected = true, Text = "All Categories", Value = "-1"}
            };
            foreach (var category in db.Categories.OrderBy(c => c.CategoryName).ToList())
            {
                lstCategories.Add(new SelectListItem { Selected = false, Text = category.CategoryName, Value = category.Id.ToString() });
            }

            homeModel.lstCategories = new SelectList(lstCategories, "Value", "Text");

            homeModel.lstCompanies = db.Companies.OrderBy(c => c.CompanyName).ToList();

            return View(homeModel);
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "Search")] HomeModel homeModel)
        {
            //var lstCategories = new List<SelectListItem>
            //{
            //    new SelectListItem {Selected = true, Text = "All Categories", Value = "-1"}
            //};
            //foreach (var category in db.Categories.OrderBy(c => c.CategoryName).ToList())
            //{
            //    lstCategories.Add(new SelectListItem { Selected = false, Text = category.CategoryName, Value = category.Id.ToString() });
            //}

            //homeModel.lstCategories = new SelectList(lstCategories, "Value", "Text");

            //homeModel.lstCompanies = db.Companies.OrderBy(c => c.CompanyName).ToList();
            return View(homeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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