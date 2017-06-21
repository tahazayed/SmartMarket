using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class Order : CommonBaseBusinessEntity
    {
        [Required]
        [Index]
        public Guid CustomerId { get; set; }


        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }


        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool IsDelivered { get; set; } = false;


        public DateTime? DeliveredDate { get; set; }
    }
}
