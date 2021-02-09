﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using C = Demo_ASP_Image.WebApp.Models;
using G = Demo_ASP_Image.DAL.Entities;

namespace Demo_ASP_Image.WebApp.Mappers
{
    public static class ImageDataMapper
    {
        public static C.ImageData ToClient(this G.ImageData global)
        {
            return new C.ImageData()
            {
                Id = global.Id,
                OriginalName = global.OriginalName,
                ImagePath = global.ImagePath
            };
        }

        public static G.ImageData ToGlobal(this C.ImageData client)
        {
            return new G.ImageData()
            {
                Id = client.Id,
                OriginalName = client.OriginalName,
                ImagePath = client.ImagePath
            };
        }
    }
}