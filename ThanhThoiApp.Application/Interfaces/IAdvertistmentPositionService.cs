using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IAdvertistmentPositionService : IDisposable
    {
        void Add(AdvertistmentPositionViewModel blog);

        void Update(AdvertistmentPositionViewModel blog);

        void Delete(string id);

        List<AdvertistmentPositionViewModel> GetAll();
        List<AdvertistmentPageViewModel> GetAllPage();

        PagedResult<AdvertistmentPositionViewModel> GetAllPaging(string keyword, int pageSize, int page);

       // List<AdvertistmentPositionViewModel> GetLastest(int top);

       // List<AdvertistmentPositionViewModel> GetHotBlog(int top);

       // List<AdvertistmentPositionViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

       // List<AdvertistmentPositionViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<AdvertistmentPositionViewModel> GetList(string keyword);

        //List<AdvertistmentPositionViewModel> GetReatedBlogs(int id, int top);

        //List<string> GetListByName(string name);

        AdvertistmentPositionViewModel GetById(string id);

        void Save();


       // void IncreaseView(int id);

        //List<AdvertistmentPositionViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        //List<TagViewModel> GetListTag(string searchText);
    }
}
