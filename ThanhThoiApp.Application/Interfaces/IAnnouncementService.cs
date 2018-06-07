using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IAnnouncementService : IDisposable
    {
        void Add(AnnouncementViewModel blog);

        void Update(AnnouncementViewModel blog);

        void Delete(string id);

        List<AnnouncementViewModel> GetAll();

        PagedResult<AnnouncementViewModel> GetAllPaging(string keyword, int pageSize, int page);


        List<AnnouncementViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<AnnouncementViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<AnnouncementViewModel> GetList(string keyword);


        List<string> GetListByName(string name);

        AnnouncementViewModel GetById(string id);

        void Save();

    }
}
