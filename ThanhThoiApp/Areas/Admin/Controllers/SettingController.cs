using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Helpers;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {
        public ISettingService _settingService;

        public SettingController(ISettingService pageService)
        {
            _settingService = pageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _settingService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _settingService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _settingService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(SystemConfigViewModel pageVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (string.IsNullOrWhiteSpace(pageVm.Id))
            {
                pageVm.Id = TextHelper.ToUnsignString(pageVm.Name);
                _settingService.Add(pageVm);
            }
            else
            {
                _settingService.Update(pageVm);
            }
            _settingService.SaveChanges();
            return new OkObjectResult(pageVm);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _settingService.Delete(id);
            _settingService.SaveChanges();

            return new OkObjectResult(id);
        }
    }
}