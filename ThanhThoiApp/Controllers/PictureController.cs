using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Models.PictureViewModels;
using ThanhThoiApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThanhThoiApp.Controllers
{
    public class PictureController : Controller
    {
        IPictureService _productService;
        ICategoryervice _Categoryervice;
        IConfiguration _configuration;
        public PictureController(IPictureService productService, IConfiguration configuration,
            ICategoryervice Categoryervice)
        {
            _productService = productService;
            _Categoryervice = Categoryervice;
            _configuration = configuration;
        }
        [Route("gallery")]
        public IActionResult Index()
        {
            var Category = _productService.GetAll();
            return View(Category);
        }

        [Route("/gallery/{alias}-c{id}")]
        public IActionResult Catalog(int id, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new CatalogViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _productService.GetAllPaging(id, string.Empty, page, pageSize.Value);
            catalog.Category = _Categoryervice.GetById(id);

            return View(catalog);
        }


        //[Route("search.html")]
        //public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        //{
        //    var catalog = new SearchResultViewModel();
        //    ViewData["BodyClass"] = "shop_grid_full_width_page";
        //    if (pageSize == null)
        //        pageSize = _configuration.GetValue<int>("PageSize");

        //    catalog.PageSize = pageSize;
        //    catalog.SortType = sortBy;
        //    catalog.Data = _productService.GetAllPaging(null, keyword, page, pageSize.Value);
        //    catalog.Keyword = keyword;

        //    return View(catalog);
        //}

        [Route("/gallery/{alias}-p{id}", Name = "PictureDetail")]
        public IActionResult Details(int id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Product = _productService.GetById(id);
            model.Category = _Categoryervice.GetById(model.Product.CategoryId);
            model.RelatedProducts = _productService.GetRelatedPicture(id, 9);
            model.ProductImages = _productService.GetImages(id);
            return View(model);
        }

    }
}