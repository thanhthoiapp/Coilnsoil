using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Application.ViewModels.System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Utilities.Helpers;
using System.Security.Claims;
using ThanhThoiApp.Extensions;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class FunctionController : BaseController
    {
        #region Initialize

        private IFunctionService _functionService;
        private IRoleService _roleService;

        public FunctionController(IFunctionService functionService, IRoleService roleService)
        {
            this._functionService = functionService;
            this._roleService = roleService;
        }

        #endregion Initialize

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _functionService.GetAll(string.Empty);
            var rootFunctions = model.Where(c => c.ParentId == null);
            var items = new List<FunctionViewModel>();

            foreach (var function in rootFunctions)
            {
                //add the parent category to the item list
                items.Add(function);
                //now get all its children (separate Category in case you need recursion)
                GetByParentId(model.ToList(), function, items);
            }
            return new ObjectResult(items);
        }
        [HttpGet]
        public IActionResult GetAllByPermission()
        {
            var roles = User.GetSpecificClaim("Roles").Split(";");
            var model =  _roleService.GetListFunctionWithRole(roles);
            var rootFunctions = model.Where(c => c.ParentId == null);
            var items = new List<FunctionViewModel>();

            foreach (var function in rootFunctions)
            {
                //add the parent category to the item list
                items.Add(function);
                //now get all its children (separate Category in case you need recursion)
                GetByParentId(model, function, items);
            }
            return new ObjectResult(items);
        }
        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _functionService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(FunctionViewModel functionVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(functionVm.Id))
                {
                    if (!string.IsNullOrEmpty(functionVm.ParentId))
                    {
                        functionVm.Id = functionVm.ParentId + "_" + TextHelper.ToUnsignString(functionVm.Name);

                    }
                    else
                    {
                        functionVm.Id = TextHelper.ToUnsignString(functionVm.Name);
                    }
                    if (_functionService.CheckExistedId(functionVm.Id))
                    {
                        functionVm.Id += "2";
                    }
                    _functionService.Add(functionVm);
                }
                else
                {
                    _functionService.Update(functionVm);
                }
                _functionService.Save();
                return new OkObjectResult(functionVm);
            }
        }

        [HttpPost]
        public IActionResult UpdateParentId(string sourceId, string targetId, Dictionary<string, int> items)
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
                    _functionService.UpdateParentId(sourceId, targetId, items);
                    _functionService.Save();
                    return new OkResult();
                }
            }
        }

        [HttpPost]
        public IActionResult ReOrder(string sourceId, string targetId)
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
                    _functionService.ReOrder(sourceId, targetId);
                    _functionService.Save();
                    return new OkObjectResult(sourceId);
                }
            }
        }


        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            else
            {
                _functionService.Delete(id);
                _functionService.Save();
                return new OkObjectResult(id);
            }
        }

        #region Private Functions

        private void GetByParentId(IEnumerable<FunctionViewModel> allFunctions,
            FunctionViewModel parent, IList<FunctionViewModel> items)
        {
            var functionsEntities = allFunctions as FunctionViewModel[] ?? allFunctions.ToArray();
            var subFunctions = functionsEntities.Where(c => c.ParentId == parent.Id);
            foreach (var cat in subFunctions)
            {
                //add this category
                items.Add(cat);
                //recursive call in case your have a hierarchy more than 1 level deep
                GetByParentId(functionsEntities, cat, items);
            }
        }

        #endregion Private Functions
    }
}