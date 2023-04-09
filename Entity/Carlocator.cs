#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraBase.Entity
{
    public class Carlocator
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CarLocatorID { get; set; }

        public string location { get; set; }

        public int CarManagementID { get; set; }
        public virtual CarManagement CarManagement { get; set; }

    }
}
