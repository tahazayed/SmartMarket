using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class OrderItem : CommonBaseBusinessEntity
    {
        [Required]
        [Index]
        public Guid OrderId { get; set; }


        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }


        [Index]
        public Guid ProductId { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public int Count { get; set; }

        [Required]
        public double PricePerItem { get; set; }
    }
}
