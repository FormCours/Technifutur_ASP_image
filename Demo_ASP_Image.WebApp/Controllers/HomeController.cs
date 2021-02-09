using Demo_ASP_Image.WebApp.Models;
using Demo_ASP_Image.WebApp.ServiceData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Demo_ASP_Image.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImage()
        {
            return View(new ImageSource());
        }

        [HttpPost]
        public ActionResult UploadImage(ImageSource source)
        {
            if (!ModelState.IsValid)
            {
                return View(source);
            }

            string fileName = Path.GetFileNameWithoutExtension(source.ImageFile.FileName).Trim();
            string fileExt = Path.GetExtension(source.ImageFile.FileName);
            string uniqueValue = Math.Abs(Guid.NewGuid().GetHashCode() % 100000).ToString("D5");

            string internalName = DateTime.Now.ToString($"yyyyMMdd_hhmmss") + "_" + fileName + "_" + uniqueValue + fileExt;
            ImageData data = new ImageData()
            {
                OriginalName = fileName + fileExt,
                ImagePath = ConfigurationManager.AppSettings["UploadImagePath"] + internalName, 
                Description = source.Description
            };

            // Save file into server
            string imageLocation = HostingEnvironment.MapPath(data.ImagePath);
            source.ImageFile.SaveAs(imageLocation);

            // Save in Database
            ImageDataService.Instance.Insert(data);

            return RedirectToAction(nameof(Index), "Home");
        }

        public ActionResult Images()
        {
            IEnumerable<ImageData> images = ImageDataService.Instance.GetAll();

            return View(images);
        }

        public ActionResult Image(Guid id)
        {
            ImageData image = ImageDataService.Instance.Get(id);

            return View(image);
        }

        public ActionResult DeleteImage(Guid id)
        {
            // Get image info
            ImageData data = ImageDataService.Instance.Get(id);
            string imageLocation = HostingEnvironment.MapPath(data.ImagePath);

            // Delete file
            if (System.IO.File.Exists(imageLocation))
                System.IO.File.Delete(imageLocation);

            // Delete row in Database
            ImageDataService.Instance.Delete(id);

            return RedirectToAction(nameof(Images));
        }

    }
}