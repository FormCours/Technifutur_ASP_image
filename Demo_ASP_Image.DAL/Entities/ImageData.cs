using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Repository;

namespace Demo_ASP_Image.DAL.Entities
{
    public class ImageData : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string OriginalName { get; set; }

        public string ImagePath { get; set; }
    }
}
