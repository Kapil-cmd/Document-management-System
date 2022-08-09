using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCoreIdentityCustom.Entity
{
    public class Role
    {
        [Key]
        public int RowId { get; set; }
        public string RequiredRole { get; set; }

        public string Username { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
