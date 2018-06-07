using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.System;
using ThanhThoiApp.Extensions;
using ThanhThoiApp.Utilities.Constants;

namespace ThanhThoiApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;
        private IRoleService _roleService;

        public SideBarViewComponent(IFunctionService functionService, IRoleService roleService)
        {
            _functionService = functionService;
            _roleService = roleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles").Split(";");
            List<FunctionViewModel> functions;
            if (roles.Contains(CommonConstants.AppRole.AdminRole))
            {
                functions = await _functionService.GetAll(string.Empty);
            }
            else
            {
                //TODO: Get by permission
                functions =  _roleService.GetListFunctionWithRole(roles);
                //functions = await _functionService.GetAllByPermission(Guid userId);
            }
            return View(functions);
        }
    }
}