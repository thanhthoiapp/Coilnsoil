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
    public class AdvertistmentPositionController : BaseController
    {
        public IAdvertistmentPositionService _advertistmentPositionService;
        private ICategoryervice _Categoryervice;

        public AdvertistmentPositionController(IAdvertistmentPositionService blogService, ICategoryervice Categoryervice)
        {
            _advertistmentPositionService = blogService;
            _Categoryervice = Categoryervice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _advertistmentPositionService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _advertistmentPositionService.GetById(id);
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllPage()
        {
            var model = _advertistmentPositionService.GetAllPage();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _advertistmentPositionService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        //[HttpPost]
        //public IActionResult SaveWholePrice(int productId, List<AdvertistmentPositionViewModel> wholePrices)
        //{
        //    _advertistmentPositionService.AddWholePrice(productId, wholePrices);
        //    _advertistmentPositionService.Save();
        //    return new OkObjectResult(wholePrices);
        //}

        //[HttpGet]
        //public IActionResult GetWholePrices()
        //{
        //    var wholePrices = _advertistmentPositionService.GetWholePrices();
        //    return new OkObjectResult(wholePrices);
        //}
        [HttpPost]
        public IActionResult SaveEntity(AdvertistmentPositionViewModel blogVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(blogVm.Id))
                {
                    blogVm.Id = TextHelper.ToUnsignString(blogVm.Name);
                    _advertistmentPositionService.Add(blogVm);
                }
                else
                {
                    _advertistmentPositionService.Update(blogVm);
                }
                _advertistmentPositionService.Save();
                return new OkObjectResult(blogVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _advertistmentPositionService.Delete(id);
            _advertistmentPositionService.Save();

            return new OkObjectResult(id);
        }
    }
}