using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [Index(IsUnique = true)]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Index]
        public bool Active { get; set; } = true;

        public bool IsSystem { get; set; } = false;

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }


        [Required]
        [MaxLength(140)]
        public string Phone { get; set; }

        public UserType UserType { get; set; } = UserType.Customer;

    }
}