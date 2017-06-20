using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BusinessEntities
{

    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Index("IX_UserId_RoleId", 1, IsUnique = true)]
        public long UserId { get; set; }
        [Index("IX_UserId_RoleId", 2, IsUnique = true)]
        public long RoleId { get; set; }

        [ForeignKey("RoleId")]
        [IgnoreDataMember]
        public virtual Role Role { get; set; }

        [ForeignKey("UserId")]
        [IgnoreDataMember]
        public virtual User User { get; set; }
    }
}