using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThanhThoiApp.Application.ViewModels.System;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IFunctionService 
    {
        void Add(FunctionViewModel function);

        Task<List<FunctionViewModel>> GetAll(string filter);
        //Task<List<PermissionViewModel>> GetAllByPermission(Guid roleId);
        PagedResult<FunctionViewModel> GetAllPaging(string keyword, int page, int pageSize);
        IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);

        FunctionViewModel GetById(string id);

        void Update(FunctionViewModel function);

        void Delete(string id);

        void Save();

        bool CheckExistedId(string id);

        void UpdateParentId(string sourceId, string targetId, Dictionary<string, int> items);

        void ReOrder(string sourceId, string targetId);
    }
}