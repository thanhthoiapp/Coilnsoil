using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Picture;
using ThanhThoiApp.Application.ViewModels.Product;

namespace ThanhThoiApp.Models.PictureViewModels
{
    public class DetailViewModel
    {
        public PictureViewModel Product { get; set; }

        public List<PictureViewModel> RelatedProducts { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<PictureDetailViewModel> ProductImages { set; get; }

        public List<PictureViewModel> LastestPictures { get; set; }

    }
}
