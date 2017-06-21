using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class Company : CommonBaseBusinessEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Index]

        public string CompanyName { get; set; }

        [Index]
        public double Rate { get; set; } = 0.0;

        [Index]
        [JsonIgnore]
        public long UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
