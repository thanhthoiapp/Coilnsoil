using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanhThoiApp.Application.Interfaces;

namespace ThanhThoiApp.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private ICommonService _commonService;
        public FooterViewComponent(ICommonService commonService)
        {
            _commonService = commonService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footer = _commonService.GetFooter();
            return View(footer);
        }
    }
}
