using System;

namespace SmartMarket.Web.Models
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public double CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public Guid DeleteId { get; set; }
    }
}