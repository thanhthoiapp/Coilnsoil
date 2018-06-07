using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IBlogService : IDisposable
    {
        BlogViewModel Add(BlogViewModel blog);

        void Update(BlogViewModel blog);

        void Delete(int id);

        List<BlogViewModel> GetAll();

        PagedResult<BlogViewModel> GetAllPaging(int? categoryId, string keyword, int pageSize, int page);
        PagedResult<BlogViewModel> GetAllPagingTag(int? categoryId, string keyword, int pageSize, int page);

        List<BlogViewModel> GetLastest(int top);
        List<BlogViewModel> GetPostByCateId(int id,int top);

        List<BlogViewModel> GetHotBlog(int top);

        List<BlogViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> GetList(string keyword);

        List<BlogViewModel> GetReatedBlogs(int id, int top);
        List<BlogViewModel> GetListPostByTagId(string tagid);

        List<string> GetListByName(string name);

        BlogViewModel GetById(int id);
        TagViewModel GetByAlias(string id);

        void Save();

        List<TagViewModel> GetListTagById(int id);

        TagViewModel GetTag(string tagId);
        List<TagViewModel> GetAllTags();

        void IncreaseView(int id);

        List<BlogViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        List<TagViewModel> GetListTag(string searchText);
    }
}
