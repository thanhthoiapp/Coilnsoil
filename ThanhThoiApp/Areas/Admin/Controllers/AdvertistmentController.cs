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
    public class AdvertistmentController : BaseController
    {
        public IAdvertistmentService _advertistmentService;
        private ICategoryervice _Categoryervice;

        public AdvertistmentController(IAdvertistmentService blogService, ICategoryervice Categoryervice)
        {
            _advertistmentService = blogService;
            _Categoryervice = Categoryervice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _advertistmentService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _advertistmentService.GetById(id);
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllPosition()
        {
            var model = _advertistmentService.GetAllPosition();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _advertistmentService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        //[HttpPost]
        //public IActionResult SaveWholePrice(int productId, List<AdvertistmentPositionViewModel> wholePrices)
        //{
        //    _advertistmentService.AddWholePrice(productId, wholePrices);
        //    _advertistmentService.Save();
        //    return new OkObjectResult(wholePrices);
        //}

        //[HttpGet]
        //public IActionResult GetWholePrices()
        //{
        //    var wholePrices = _advertistmentService.GetWholePrices();
        //    return new OkObjectResult(wholePrices);
        //}
        [HttpPost]
        public IActionResult SaveEntity(AdvertistmentViewModel blogVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (blogVm.Id == 0)
                {
                    _advertistmentService.Add(blogVm);
                }
                else
                {
                    _advertistmentService.Update(blogVm);
                }
                _advertistmentService.Save();
                return new OkObjectResult(blogVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _advertistmentService.Delete(id);
            _advertistmentService.Save();

            return new OkObjectResult(id);
        }
    }
}