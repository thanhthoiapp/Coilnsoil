using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface ISettingService
    {
        void Add(SystemConfigViewModel feedbackVm);

        void Update(SystemConfigViewModel feedbackVm);

        void Delete(string id);

        List<SystemConfigViewModel> GetAll();

        PagedResult<SystemConfigViewModel> GetAllPaging(string keyword, int page, int pageSize);

        SystemConfigViewModel GetById(string id);

        void SaveChanges();
    }
}
