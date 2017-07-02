using BusinessEntities;
using System.Collections.Generic;

namespace SmartMarket.Web.Models
{
    public class OrderModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public long UserId { get; set; }
    }

}