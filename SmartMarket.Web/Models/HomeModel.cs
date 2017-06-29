using BusinessEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmartMarket.Web.Models
{
    public class HomeModel
    {
        public SelectList lstCategories { get; set; }
        public List<Company> lstCompanies { get; set; }
        public string Search { get; set; }
    }
}