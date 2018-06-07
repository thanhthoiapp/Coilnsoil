using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface ISlideService : IDisposable
    {
        void Add(SlideViewModel slide);

        void Update(SlideViewModel slide);

        void Delete(int id);

        List<SlideViewModel> GetAll();

        PagedResult<SlideViewModel> GetAllPaging(string keyword, int pageSize, int page);


        List<SlideViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<SlideViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<SlideViewModel> GetList(string keyword);


        List<string> GetListByName(string name);

        SlideViewModel GetById(int id);

        void SaveChanges();

    }
}
