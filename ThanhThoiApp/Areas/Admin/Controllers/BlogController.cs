using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Extensions;
using ThanhThoiApp.Utilities.Helpers;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        public IBlogService _blogService;
        private ICategoryervice _Categoryervice;

        public BlogController(IBlogService blogService, ICategoryervice Categoryervice)
        {
            _blogService = blogService;
            _Categoryervice = Categoryervice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _blogService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _blogService.GetById(id);

            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var model = _Categoryervice.GetAllBlog().Where(x => x.Type == 1);
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var model = _blogService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(BlogViewModel blogVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                Guid idCus = Guid.Parse(User.GetSpecificClaim("UserId"));
                blogVm.PostById = idCus;
                if(blogVm.SeoAlias == null)
                {
                    blogVm.SeoAlias = TextHelper.ToUnsignString(blogVm.Name);
                }
                else
                {
                    blogVm.SeoAlias = TextHelper.ToUnsignString(blogVm.SeoAlias);
                }
                if (blogVm.Id == 0)
                {
                    _blogService.Add(blogVm);
                }
                else
                {
                    _blogService.Update(blogVm);
                }
                _blogService.Save();
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
            _blogService.Delete(id);
            _blogService.Save();

            return new OkObjectResult(id);
        }
    }
}