using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class DocumentEntity
    {
        [Key]
        [Required]
        public int DocumentId { get; set; }
        [MaxLength(50, ErrorMessage = "Title cannot be more than 50")]

        [Required]
        public string Title { get; set; }

        [MaxLength(250, ErrorMessage = "Title cannot be more than 250")]
        [Display(Name ="Document Name")]
        [Required]
        public string DocumentName { get; set; }
        
        [Display(Name = "Document Creater Name")]
        public string DocumentCreaterName { get; set; }
        [Required]
        [MaxLength(5050, ErrorMessage = "Title cannot be more than 250")]
        [Display(Name = "Description")]
        public string Discription { get; set; }

        [Required]
        [Display(Name ="Created date:")]
        public DateTime DocumentCreatedDate { get; set; }

        [Required]
        [Display(Name = "Modified date")]
        public DateTime DocumentModifiedDate { get; set; }

        [NotMapped]
        [Display(Name = "Upload File")]
        public List<IFormFile> DocumentFiles { get; set; }

        public string UserId { get; set; }

        public string Image1Path { get; set; }
        public string Image2Path { get; set; }
        public string Image3Path { get; set; }
        public string Image4Path { get; set; }
        public string Image5Path { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}

