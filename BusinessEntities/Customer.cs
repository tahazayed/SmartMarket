using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class Customer : CommonBaseBusinessEntity
    {
        [Required]
        [StringLength(1000, MinimumLength = 2)]
        [Index]

        public string Nikename { get; set; }

        public Gender Gender { get; set; } = Gender.Male;

        [Index]
        public long UserId { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}
