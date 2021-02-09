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
            return View(new ImageSourceNew());
        }

        [HttpPost]
        public ActionResult UploadImage(ImageSourceNew source)
        {
            if (!ModelState.IsValid)
            {
                return View(source);
            }

            string fileLocation = GenerateInternalFilName(source.ImageFile);
            ImageData data = new ImageData()
            {
                OriginalName = Path.GetFileName(source.ImageFile.FileName),
                ImagePath = fileLocation,
                Description = source.Description
            };

            // Save file into server
            string imageLocation = HostingEnvironment.MapPath(data.ImagePath);
            source.ImageFile.SaveAs(imageLocation);

            // Save in Database
            ImageDataService.Instance.Insert(data);

            return RedirectToAction(nameof(Index), "Home");
        }

        private string GenerateInternalFilName(HttpPostedFileBase file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName).Trim();
            string fileExt = Path.GetExtension(file.FileName);
            string uniqueValue = Math.Abs(Guid.NewGuid().GetHashCode() % 100000).ToString("D5");

            string internalName = DateTime.Now.ToString($"yyyyMMdd_hhmmss") + "_" + fileName + "_" + uniqueValue + fileExt;
            return ConfigurationManager.AppSettings["UploadImagePath"] + internalName;
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

        public ActionResult Delete(Guid id)
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

        public ActionResult Update(Guid id)
        {
            ImageData data = ImageDataService.Instance.Get(id);

            ImageSourceUpdate sourceUpdate = new ImageSourceUpdate()
            {
                OriginalName = data.OriginalName,
                Description = data.Description
            };
            return View(sourceUpdate);
        }

        [HttpPost]
        public ActionResult Update(Guid id, ImageSourceUpdate source)
        { 
            if(!ModelState.IsValid)
            {
                return View(source);
            }
            
            ImageData oldData = ImageDataService.Instance.Get(id);

            string newImage = null;
            if(source.ImageFile != null)
            {
                // Delete old image
                string oldImage = HostingEnvironment.MapPath(oldData.ImagePath);
                if (System.IO.File.Exists(oldImage))
                    System.IO.File.Delete(oldImage);

                // Save new image
                newImage = GenerateInternalFilName(source.ImageFile);
                source.ImageFile.SaveAs(HostingEnvironment.MapPath(newImage));
            }

            // Update row in Database
            ImageData newData = new ImageData()
            {
                OriginalName = source.OriginalName,
                ImagePath = newImage ?? oldData.ImagePath,
                Description = source.Description
            };
            ImageDataService.Instance.Update(id, newData);

            return RedirectToAction(nameof(Image), new { id = id });
        }
    }
}