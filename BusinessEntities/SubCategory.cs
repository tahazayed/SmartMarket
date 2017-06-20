using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class SubCategory : CommonBaseBusinessEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [Index]
        [Index("IX_Name_CategoryIdId", IsUnique = true, Order = 1)]
        public string SubCategoryName { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Index("IX_Name_CategoryIdId", IsUnique = true, Order = 2)]
        public Guid CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
