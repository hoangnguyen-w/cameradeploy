#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraBase.Entity
{
    public class NotifiHistory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HistoryID { get; set; }

        [MaxLength(100)]
        public String HistoryName { get; set; }

        public DateTime HistoryDate { get;set; }    

        public bool HistoryStatus { get; set; }

        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        public int CarManagementID { get; set; }
        public virtual CarManagement CarManagement { get; set; }
    }
}
