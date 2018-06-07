using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThanhThoiApp.Utilities.Helpers;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        ICategoryervice _Categoryervice;
        public CategoryController(ICategoryervice Categoryervice)
        {
            _Categoryervice = Categoryervice;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Get Data API
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _Categoryervice.GetById(id);

            return new ObjectResult(model);
        }
        [HttpPost]
        public IActionResult SaveEntity(CategoryViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
                if (productVm.Id == 0)
                {
                    if(productVm.Type == 1)
                    {
                        productVm.Path = "/" + TextHelper.ToUnsignString(productVm.Name);
                    }
                    else if (productVm.Type == 2)
                    {
                        productVm.Path = "/" + "san-pham" + "/" + TextHelper.ToUnsignString(productVm.Name);
                    }
                    else if (productVm.Type == 3)
                    {
                        productVm.Path = "/" + "page" + "/" + TextHelper.ToUnsignString(productVm.Name);
                    }
                    else if (productVm.Type == 4)
                    {
                        productVm.Path = "/";
                    }
                    else if (productVm.Type == 5)
                    {
                        productVm.Path = "/" + TextHelper.ToUnsignString(productVm.Name);
                    }
                    else if (productVm.Type == 6)
                    {
                        productVm.Path = "/gallery/" + TextHelper.ToUnsignString(productVm.Name);
                    }
                    _Categoryervice.Add(productVm);
                }
                else
                {
                    _Categoryervice.Update(productVm);
                }
                _Categoryervice.Save();
                return new OkObjectResult(productVm);

            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                _Categoryervice.Delete(id);
                _Categoryervice.Save();
                return new OkObjectResult(id);
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _Categoryervice.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllGalerry()
        {
            var model = _Categoryervice.GetAll().Where(x => x.Type == 6);
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllBlog()
        {
            var model = _Categoryervice.GetAll().Where(x => x.Type == 1);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _Categoryervice.UpdateParentId(sourceId, targetId, items);
                    _Categoryervice.Save();
                    return new OkResult();
                }
            }
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _Categoryervice.ReOrder(sourceId, targetId);
                    _Categoryervice.Save();
                    return new OkResult();
                }
            }
        }

        #endregion
    }
}