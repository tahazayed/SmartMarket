using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class Category : CommonBaseBusinessEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [Index]
        public string CategoryName { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
