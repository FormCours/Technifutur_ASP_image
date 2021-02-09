using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_ASP_Image.WebApp.Models
{
    public class ImageSourceNew
    {
        [Required]
        [DisplayName("Image")]
        public HttpPostedFileBase ImageFile { get; set; } // For upload file

        [DisplayName("Description")]
        public string Description { get; set; }
    }

    public class ImageSourceUpdate
    {
        [Required]
        [DisplayName("Nom du fichier")]
        public string OriginalName { get; set; }

        [DisplayName("Image (Uniquement si vous souhaitez changer l'image)")]
        public HttpPostedFileBase ImageFile { get; set; } // For upload file

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}