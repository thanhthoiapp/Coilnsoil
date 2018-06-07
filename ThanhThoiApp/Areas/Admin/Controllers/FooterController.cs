using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Common;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        public IFooterService _footerService;

        public FooterController(IFooterService pageService)
        {
            _footerService = pageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _footerService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _footerService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _footerService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(FooterViewModel pageVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (string.IsNullOrWhiteSpace(pageVm.Id))
            {
                _footerService.Add(pageVm);
            }
            else
            {
                _footerService.Update(pageVm);
            }
            _footerService.SaveChanges();
            return new OkObjectResult(pageVm);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _footerService.Delete(id);
            _footerService.SaveChanges();

            return new OkObjectResult(id);
        }
    }
}