using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Models.ProductViewModels;
using ThanhThoiApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThanhThoiApp.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IBillService _billService;
        ICategoryervice _Categoryervice;
        IConfiguration _configuration;
        public ProductController(IProductService productService, IConfiguration configuration,
            IBillService billService,
            ICategoryervice Categoryervice)
        {
            _productService = productService;
            _Categoryervice = Categoryervice;
            _configuration = configuration;
            _billService = billService;
        }
        [Route("san-pham")]
        public IActionResult Index()
        {
            var Category = _Categoryervice.GetAllProduct();
            return View(Category);
        }

        [Route("/san-pham/{alias}-c.{id}")]
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

        [Route("/san-pham/{alias}-p.{id}", Name = "ProductDetail")]
        public IActionResult Details(int id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Product = _productService.GetById(id);
            model.Category = _Categoryervice.GetById(model.Product.CategoryId);
            model.RelatedProducts = _productService.GetRelatedProducts(id, 9);
            model.UpsellProducts = _productService.GetUpsellProducts(6);
            model.ProductImages = _productService.GetImages(id);
            model.Tags = _productService.GetProductTags(id);
            model.Colors = _billService.GetColors().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            model.Sizes = _billService.GetSizes().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

    }
}