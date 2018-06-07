using AutoMapper;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Product;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Infrastructure.Interfaces;
using ThanhThoiApp.Utilities.Constants;
using ThanhThoiApp.Utilities.Dtos;
using ThanhThoiApp.Utilities.Helpers;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Picture;

namespace ThanhThoiApp.Application.Implementation
{
    public class PictureService : IPictureService
    {
        private IRepository<Picture, int> _productRepository;
        private IRepository<PictureDetail, int> _productImageRepository;

        IUnitOfWork _unitOfWork;
        public PictureService(IRepository<Picture, int> productRepository,
            IRepository<PictureDetail, int> productImageRepository,
        IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _unitOfWork = unitOfWork;
        }

        public PictureViewModel Add(PictureViewModel productVm)
        {
           var product = Mapper.Map<PictureViewModel, Picture>(productVm);
                _productRepository.Add(product);
            return productVm;
        }


        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public List<PictureViewModel> GetPostByCateId(int id, int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active).Where(x => x.CategoryId == id || x.Category.ParentId == id).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<PictureViewModel>().ToList();
        }
        public List<PictureDetailViewModel> GetPictureLatest(int top)
        {
            return _productImageRepository.FindAll().OrderByDescending(x => x.Id)
                .Take(top).ProjectTo<PictureDetailViewModel>().ToList();
        }
        public List<PictureViewModel> GetAll()
        {
            return _productRepository.FindAll(x => x.Category).ProjectTo<PictureViewModel>().ToList();
        }

        public PagedResult<PictureViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateModified)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<PictureViewModel>().ToList();

            var paginationSet = new PagedResult<PictureViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public PictureViewModel GetById(int id)
        {
            return Mapper.Map<Picture, PictureViewModel>(_productRepository.FindById(id));
        }

 
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PictureViewModel productVm)
        {
            var product = Mapper.Map<PictureViewModel, Picture>(productVm);
            _productRepository.Update(product);
        }

        public List<PictureDetailViewModel> GetImages(int productId)
        {
            return _productImageRepository.FindAll(x => x.PictureId == productId)
                .ProjectTo<PictureDetailViewModel>().ToList();
        }

        public void AddImages(int productId, string[] images)
        {
            _productImageRepository.RemoveMultiple(_productImageRepository.FindAll(x => x.PictureId == productId).ToList());
            foreach (var image in images)
            {
                _productImageRepository.Add(new PictureDetail()
                {
                    Name = image,
                    PictureId = productId,
                    HomeFlag = true,
                    IsAvatar = true
                });
            }

        }

        public List<PictureViewModel> GetLastest(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<PictureViewModel>().ToList();
        }

        public List<PictureViewModel> GetHotPicture(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active && x.HomeFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<PictureViewModel>()
                .ToList();
        }

        public List<PictureViewModel> GetRelatedPicture(int id, int top)
        {
            var product = _productRepository.FindById(id);
            return _productRepository.FindAll(x => x.Status == Status.Active
                && x.Id != id && x.CategoryId == product.CategoryId)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<PictureViewModel>()
            .ToList();
        }
    }
}
