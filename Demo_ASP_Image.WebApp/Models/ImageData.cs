using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo_ASP_Image.WebApp.Models
{
    public class ImageData
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [DisplayName("Nom du fichier")]
        public string OriginalName { get; set; }

        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [DisplayName("Description")]
        [DisplayFormat(NullDisplayText = "Pas de description...")]
        public string Description { get; set; }
    }
}