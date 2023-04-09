#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraBase.Entity
{
    public class Account
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter Name")]
        public string AccounName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter password ")]
        public string password { get; set; }

        [MaxLength(200)]    
        public string AccountEmail { get; set; }
        
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        public string Image { get; set; }

        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<NotifiHistory> NotifiHistories { get; set; }

        //RefreshToken "If you gonna migration comment this!"
        public string RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

    }
}
