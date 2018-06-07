using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Models.BlogViewModels;
using ThanhThoiApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThanhThoiApp.Application.ViewModels.Blog;

namespace ThanhThoiApp.Controllers
{
    public class BlogController : Controller
    {
        IBlogService _blogService;
        ICategoryervice _Categoryervice;
        IConfiguration _configuration;
        public BlogController(IBlogService blogService, IConfiguration configuration,
            ICategoryervice Categoryervice)
        {
            _blogService = blogService;
            _Categoryervice = Categoryervice;
            _configuration = configuration;
        }
        [Route("bai-viet")]
        public IActionResult Index()
        {
            var Category = _blogService.GetAll();
            return View(Category);
        }

        [Route("{alias}/")]
        public IActionResult Catalog(string alias, int? pageSize, int page = 1)
        {
            var catalog = new BlogCViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");
            var category = _Categoryervice.GetByAlias(alias);
            catalog.PageSize = pageSize;
            catalog.Data = _blogService.GetAllPaging(category.Id, string.Empty, page, pageSize.Value);
            catalog.Category = category;
            catalog.GetLastest = _blogService.GetLastest(5);
            catalog.Tags = _blogService.GetAllTags();

            return View(catalog);
        }


        [Route("search.html")]
        public IActionResult Search(string keyword, int? pageSize, int page = 1)
        {
            var catalog = new SearchResultBlogViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.Data = _blogService.GetAllPaging(null, keyword, page, pageSize.Value);
            catalog.Keyword = keyword;

            return View(catalog);
        }

        [Route("/{SeoAlias}/{alias}-{id}", Name = "BlogDetail")]
        public IActionResult Details(int id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Blog = _blogService.GetById(id);
            model.Category = _Categoryervice.GetById(model.Blog.CategoryId);
            model.GetLastest = _blogService.GetLastest(5);
            model.Tags = _blogService.GetListTagById(id);
            _blogService.IncreaseView(id);
            return View(model);
        }

        [Route("/tag/{tagId}", Name = "Tag")]
        public IActionResult Tags(string tagId, int? pageSize, int page = 1)
        {
            var catalog = new TagsViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            catalog.Data = _blogService.GetListPostByTagId(tagId);
            catalog.Title = _blogService.GetByAlias(tagId).Name;
            return View(catalog);
        }
    }
}