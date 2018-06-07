using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Product;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Infrastructure.Enums;

namespace ThanhThoiApp.Controllers.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private ICategoryervice _Categoryervice;
        private IMemoryCache _memoryCache;
        public CategoryMenuViewComponent(ICategoryervice Categoryervice,IMemoryCache memoryCache)
        {
            _Categoryervice = Categoryervice;
            _memoryCache = memoryCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Category = _memoryCache.GetOrCreate(CacheKeys.Category, entry => {
                entry.SlidingExpiration = TimeSpan.FromHours(2);
                return _Categoryervice.GetAll();
            });
            
            return View(Category);
        }
    }
}
