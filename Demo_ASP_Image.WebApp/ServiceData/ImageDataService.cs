using Demo_ASP_Image.DAL.Repositories;
using Demo_ASP_Image.WebApp.Mappers;
using Demo_ASP_Image.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_ASP_Image.WebApp.ServiceData
{
    public class ImageDataService
    {
        #region Singleton
        private static Lazy<ImageDataService> _LazyInstance = new Lazy<ImageDataService>(() => new ImageDataService());

        public static ImageDataService Instance
        {
            get { return _LazyInstance.Value; }
        }
        #endregion

        private ImageDataRepository repo;

        private ImageDataService()
        {
            repo = new ImageDataRepository();
        }

        public Guid Insert(ImageData data)
        {
            return repo.Insert(data.ToGlobal());
        }

        public ImageData Get(Guid id)
        {
            return repo.Get(id).ToClient();
        }

        public IEnumerable<ImageData> GetAll()
        {
            return repo.GetAll().Select(elem => elem.ToClient());
        }

        public void Update(Guid id, ImageData data)
        {
            repo.Update(id, data.ToGlobal());
        }

        public void Delete(Guid id)
        {
            repo.Delete(id);
        }
    }
}