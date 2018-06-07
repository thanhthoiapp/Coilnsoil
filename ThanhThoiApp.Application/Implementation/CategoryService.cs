using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Product;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Infrastructure.Interfaces;

namespace ThanhThoiApp.Application.Implementation
{
    public class Categoryervice : ICategoryervice
    {
        private IRepository<Category, int> _CategoryRepository;
        private IRepository<Product, int> _ProductRepository;
        private IUnitOfWork _unitOfWork;

        public Categoryervice(IRepository<Category, int> CategoryRepository, IRepository<Product, int> ProductRepository,
            IUnitOfWork unitOfWork)
        {
            _CategoryRepository = CategoryRepository;
            _ProductRepository = ProductRepository;
            _unitOfWork = unitOfWork;
        }

        public CategoryViewModel Add(CategoryViewModel CategoryVm)
        {
            var Category = Mapper.Map<CategoryViewModel, Category>(CategoryVm);
            _CategoryRepository.Add(Category);
            return CategoryVm;

        }

        public void Delete(int id)
        {
            _CategoryRepository.Remove(id);
        }

        public List<CategoryViewModel> GetAll()
        {
            return _CategoryRepository.FindAll().OrderBy(x => x.ParentId)
                 .ProjectTo<CategoryViewModel>().ToList();
        }
        public List<CategoryViewModel> GetAllProduct()
        {
            return _CategoryRepository.FindAll().OrderBy(x => x.ParentId).Where(x => x.Type == 2 && x.ParentId != null)
                 .ProjectTo<CategoryViewModel>().ToList();
        }
        public List<CategoryViewModel> GetAllBlog()
        {
            return _CategoryRepository.FindAll().OrderBy(x => x.ParentId).Where(x => x.Type == 1)
                 .ProjectTo<CategoryViewModel>().ToList();
        }

        public List<CategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _CategoryRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId).ProjectTo<CategoryViewModel>().ToList();
            else
                return _CategoryRepository.FindAll().OrderBy(x => x.ParentId)
                    .ProjectTo<CategoryViewModel>()
                    .ToList();
        }

        public List<CategoryViewModel> GetAllByParentId(int parentId)
        {
            return _CategoryRepository.FindAll(x => x.Status == Status.Active
            && x.ParentId == parentId)
             .ProjectTo<CategoryViewModel>()
             .ToList();
        }
        public CategoryViewModel GetByAlias(string alias)
        {
            return Mapper.Map<Category, CategoryViewModel>(_CategoryRepository.FindSingle(x => x.SeoAlias == alias));
        }
        public CategoryViewModel GetById(int id)
        {
            return Mapper.Map<Category, CategoryViewModel>(_CategoryRepository.FindById(id));
        }

        public List<CategoryViewModel> GetHomeCategory(int top)
        {
            var query = _CategoryRepository
                .FindAll(x => x.HomeFlag == true, c => c.Products)
                  .OrderBy(x => x.HomeOrder)
                  .Take(top).ProjectTo<CategoryViewModel>();

            var Category = query.ToList();
            foreach (var category in Category)
            {
                category.Products = _ProductRepository.FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(5)
                    .ProjectTo<ProductViewModel>().ToList();
            }
            return Category;
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _CategoryRepository.FindById(sourceId);
            var target = _CategoryRepository.FindById(targetId);
            int tempOrder = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _CategoryRepository.Update(source);
            _CategoryRepository.Update(target);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(CategoryViewModel CategoryVm)
        {
            var Category = Mapper.Map<CategoryViewModel, Category>(CategoryVm);
            _CategoryRepository.Update(Category);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sourceCategory = _CategoryRepository.FindById(sourceId);
            sourceCategory.ParentId = targetId;
            _CategoryRepository.Update(sourceCategory);

            //Get all sibling
            var sibling = _CategoryRepository.FindAll(x => items.ContainsKey(x.Id));
            foreach(var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _CategoryRepository.Update(child);
            }
        }
    }
}
