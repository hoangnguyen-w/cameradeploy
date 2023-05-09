#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraBase.Entity
{
    public class SubAccount
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SubAccountID { get; set; }

        public string SubAccountName { get; set; }

        public string SubAccountPhone { get; set; }

        public int AccountID { get; set; }

        //public bool Status { get; set; }

        public virtual Account Account { get; set; }

    }
}
