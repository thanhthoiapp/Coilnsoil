using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IFooterService
    {
        void Add(FooterViewModel feedbackVm);

        void Update(FooterViewModel feedbackVm);

        void Delete(string id);

        List<FooterViewModel> GetAll();

        PagedResult<FooterViewModel> GetAllPaging(string keyword, int page, int pageSize);

        FooterViewModel GetById(string id);

        void SaveChanges();
    }
}
