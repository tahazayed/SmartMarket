using BusinessEntities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SmartMarket.Web.Helpers
{
    public class CommonHelper
    {
        public SelectList GetNonEmptyCategories()
        {
            using (SmartMarketDB db = new SmartMarketDB())
            {
                var lstCategories = new List<SelectListItem>
                {
                    new SelectListItem {Selected = true, Text = "All Categories", Value = "-1"}
                };
                var categories = (from p in db.Products
                                  select p.SubCategory.Category).Distinct().OrderBy(c => c.CategoryName).ToList();
                ;
                foreach (var category in categories)
                {
                    lstCategories.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = category.CategoryName,
                        Value = category.Id.ToString()
                    });
                }
                return new SelectList(lstCategories, "Value", "Text");

            }
        }
    }
}