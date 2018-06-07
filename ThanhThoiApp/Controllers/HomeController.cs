using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Models;
using Microsoft.AspNetCore.Authorization;
using ThanhThoiApp.Extensions;
using ThanhThoiApp.Application.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace ThanhThoiApp.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryervice _Categoryervice;

        private IBlogService _blogService;
        private IPictureService _pictureService;
        private ICommonService _commonService;
        private ISettingService _settingService;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IProductService productService, ISettingService settingService,
        IBlogService blogService, IPictureService pictureService, ICommonService commonService,
       ICategoryervice Categoryervice, IStringLocalizer<HomeController> localizer)
        {
            _blogService = blogService;
            _pictureService = pictureService;
            _commonService = commonService;
            _settingService = settingService;
            _productService = productService;
            _Categoryervice = Categoryervice;
            _localizer = localizer;
        }

        //[ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            var title = _localizer["Title"];
            var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            ViewData["BodyClass"] = "cms-index-index cms-home-page";
            var homeVm = new HomeViewModel();
            homeVm.HomeCategory = _Categoryervice.GetHomeCategory(5);
            homeVm.HotProducts = _productService.GetHotProduct(5);
            homeVm.TopSellProducts = _productService.GetLastest(5);
            homeVm.LastestBlogs = _blogService.GetLastest(5);
            homeVm.TopService = _blogService.GetPostByCateId(3,10);
            homeVm.TopProject = _pictureService.GetPostByCateId(13, 10);
            homeVm.TopProjectPhoto = _pictureService.GetPictureLatest(10);
            homeVm.TopNews = _blogService.GetPostByCateId(1, 10);
            homeVm.HomeSlides = _commonService.GetSlides("top");
            homeVm.HomeBrand = _commonService.GetSlides("brand");
            homeVm.Title = _commonService.GetSystemConfig("HomeTitle").Value1;
            homeVm.MetaKeyword = _commonService.GetSystemConfig("HomeMetaKeyword").Value1;
            homeVm.MetaDescription = _commonService.GetSystemConfig("HomeMetaDescription").Value1;
            homeVm.Footer = _commonService.GetFooter();
            return View(homeVm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Intro()
        {
            var title = _localizer["Title"];
            var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            ViewData["Message"] = "Intro Page";
            var homeVm = new HomeViewModel();
            homeVm.Title = _commonService.GetSystemConfig("HomeTitle").Value1;
            homeVm.MetaKeyword = _commonService.GetSystemConfig("HomeMetaKeyword").Value1;
            homeVm.MetaDescription = _commonService.GetSystemConfig("HomeMetaDescription").Value1;
            homeVm.IntroFooterId = _commonService.GetIntro();
            return View(homeVm);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
