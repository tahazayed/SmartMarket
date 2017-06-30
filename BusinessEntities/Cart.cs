using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class Cart : CommonBaseBusinessEntity
    {
        public long CartId { get; set; }

        [Index]
        public Guid ProductId { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
