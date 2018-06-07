using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Product;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface ICategoryervice
    {
        CategoryViewModel Add(CategoryViewModel CategoryVm);

        void Update(CategoryViewModel CategoryVm);

        void Delete(int id);

        List<CategoryViewModel> GetAll();
        List<CategoryViewModel> GetAllProduct();

        List<CategoryViewModel> GetAllBlog();

        List<CategoryViewModel> GetAll(string keyword);

        List<CategoryViewModel> GetAllByParentId(int parentId);

        CategoryViewModel GetById(int id);
        CategoryViewModel GetByAlias(string alias);

        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);
        void ReOrder(int sourceId, int targetId);

        List<CategoryViewModel> GetHomeCategory(int top);

       


        void Save();
    }
}
