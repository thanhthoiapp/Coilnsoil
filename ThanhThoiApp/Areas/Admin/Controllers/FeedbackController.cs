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
    public class FeedbackController : BaseController
    {
        public IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService pageService)
        {
            _feedbackService = pageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _feedbackService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _feedbackService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _feedbackService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(FeedbackViewModel pageVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (pageVm.Id == 0)
            {
                _feedbackService.Add(pageVm);
            }
            else
            {
                _feedbackService.Update(pageVm);
            }
            _feedbackService.SaveChanges();
            return new OkObjectResult(pageVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _feedbackService.Delete(id);
            _feedbackService.SaveChanges();

            return new OkObjectResult(id);
        }
    }
}