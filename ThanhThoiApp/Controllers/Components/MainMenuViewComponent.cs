using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanhThoiApp.Application.Interfaces;

namespace ThanhThoiApp.Controllers.Components
{
    public class MainMenuViewComponent : ViewComponent
    {

        private ICategoryervice _Categoryervice;

        public MainMenuViewComponent(ICategoryervice Categoryervice)
        {
            _Categoryervice = Categoryervice;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_Categoryervice.GetAll());
        }
    }
}
