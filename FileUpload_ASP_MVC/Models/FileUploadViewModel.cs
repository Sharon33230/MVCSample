using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FileUpload_ASP_MVC.Models
{
    public class FileUploadViewModel
    {
        [Required]
        [Display(Name = "Choose File")]
        public HttpPostedFileBase File { get; set; } // For single file upload
    }
}

