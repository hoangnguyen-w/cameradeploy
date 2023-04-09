#nullable disable
using System.ComponentModel.DataAnnotations;

namespace CameraBase.Entity
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "Enter Role Name")]
        public string RoleName { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
