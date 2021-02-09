using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_ASP_Image.WebApp.Models
{
    public class ImageSource
    {
        // For upload file

        [Required]
        [DisplayName("Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}