using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Extensions;
using ThanhThoiApp.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class AnnouncementController : BaseController
    {
        public IAnnouncementService _announcementService;
        public IUserService _userService;


        public AnnouncementController(IAnnouncementService announcementService, IUserService userService)
        {
            _announcementService = announcementService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _announcementService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _announcementService.GetById(id);

            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _announcementService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(AnnouncementViewModel blogVm)
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
                    Guid iduser = Guid.Parse(User.GetSpecificClaim("UserId"));
                    blogVm.UserId = iduser;
                    blogVm.Id = TextHelper.ToUnsignString(blogVm.Title);
                    _announcementService.Add(blogVm);
                }
                else
                {
                    _announcementService.Update(blogVm);
                }
                _announcementService.Save();
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
            _announcementService.Delete(id);
            _announcementService.Save();

            return new OkObjectResult(id);
        }
    }
}