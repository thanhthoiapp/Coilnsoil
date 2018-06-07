using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Product;

namespace ThanhThoiApp.Models.BlogViewModels
{
    public class DetailViewModel
    {
        public BlogViewModel Blog { get; set; }

        public List<BlogViewModel> GetLastest { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<TagViewModel> Tags { set; get; }

    }
}
