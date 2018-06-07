using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IPageService : IDisposable
    {
        void Add(PageViewModel pageVm);

        void Update(PageViewModel pageVm);

        void Delete(int id);

        List<PageViewModel> GetAll();

        PagedResult<PageViewModel> GetAllPaging(string keyword, int page, int pageSize);

        PageViewModel GetByAlias(string alias);

        PageViewModel GetById(int id);

        void SaveChanges();

    }
}
