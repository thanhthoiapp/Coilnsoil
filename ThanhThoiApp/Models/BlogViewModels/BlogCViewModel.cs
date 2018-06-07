using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Product;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Models.BlogViewModels
{
    public class BlogCViewModel
    {
        public PagedResult<BlogViewModel> Data { get; set; }
        public List<BlogViewModel> GetLastest { get; set; }
        public List<TagViewModel> Tags { set; get; }
        public CategoryViewModel Category { set; get; }

        public int? PageSize { set; get; }

        public List<SelectListItem> PageSizes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "12",Text = "12"},
            new SelectListItem(){Value = "24",Text = "24"},
            new SelectListItem(){Value = "48",Text = "48"},
        };
    }
}
