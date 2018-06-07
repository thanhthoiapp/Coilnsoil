using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IAdvertistmentService : IDisposable
    {
        void Add(AdvertistmentViewModel blog);

        void Update(AdvertistmentViewModel blog);

        void Delete(int id);

        List<AdvertistmentViewModel> GetAll();
        List<AdvertistmentPositionViewModel> GetAllPosition();

        PagedResult<AdvertistmentViewModel> GetAllPaging(string keyword, int pageSize, int page);

       // List<AdvertistmentViewModel> GetLastest(int top);

       // List<AdvertistmentViewModel> GetHotBlog(int top);

       // List<AdvertistmentViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

       // List<AdvertistmentViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<AdvertistmentViewModel> GetList(string keyword);

        //List<AdvertistmentViewModel> GetReatedBlogs(int id, int top);

        List<string> GetListByName(string name);

        AdvertistmentViewModel GetById(int id);

        void Save();


       // void IncreaseView(int id);

        //List<AdvertistmentViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        //List<TagViewModel> GetListTag(string searchText);
    }
}
