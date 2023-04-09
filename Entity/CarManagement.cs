#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraBase.Entity
{
    public class CarManagement
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CarManagementID { get; set; }

        [MaxLength(100)]
        public string CarName { get; set; }

        [MaxLength(100)]
        public string CarColor { get; set; }

        [MaxLength(100)]
        public string LicensePlates { get; set; }

        [MaxLength(100)]
        public string CarBrand { get; set; }

        public virtual ICollection<NotifiHistory> NotifiHistories { get; set; }
        
        public virtual ICollection<Carlocator> Carlocators { get; set; }
    }
}
