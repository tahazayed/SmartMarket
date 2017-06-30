using BusinessEntities;
using System.Collections.Generic;

namespace SmartMarket.Web.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}