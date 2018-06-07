using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Infrastructure.Interfaces;
using ThanhThoiApp.Utilities.Constants;
using ThanhThoiApp.Utilities.Dtos;
using ThanhThoiApp.Utilities.Helpers;

namespace ThanhThoiApp.Application.Implementation
{
    public class AdvertistmentPositionService : IAdvertistmentPositionService
    {
        IRepository<AdvertistmentPosition, string> _advertistmentPositionRepository;
        IRepository<AdvertistmentPage, string> _advertistmentPageRepository;
        IUnitOfWork _unitOfWork;

        public AdvertistmentPositionService(IRepository<AdvertistmentPosition, string> advertistmentPositionRepository, IRepository<AdvertistmentPage, string> advertistmentPageRepository,
            IUnitOfWork unitOfWork)
        {
            _advertistmentPositionRepository = advertistmentPositionRepository;
            _advertistmentPageRepository = advertistmentPageRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Add(AdvertistmentPositionViewModel pageVm)
        {
            var page = Mapper.Map<AdvertistmentPositionViewModel, AdvertistmentPosition>(pageVm);
            _advertistmentPositionRepository.Add(page);
        }

        public void Delete(string id)
        {
            _advertistmentPositionRepository.Remove(id);
        }

        public List<AdvertistmentPositionViewModel> GetAll()
        {
            return _advertistmentPositionRepository.FindAll(c => c.Id)
                .ProjectTo<AdvertistmentPositionViewModel>().ToList();
        }

        public List<AdvertistmentPageViewModel> GetAllPage()
        {
            return _advertistmentPageRepository.FindAll().OrderBy(x => x.Id).ProjectTo<AdvertistmentPageViewModel>().ToList();
        }

        public PagedResult<AdvertistmentPositionViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _advertistmentPositionRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<AdvertistmentPositionViewModel>()
            {
                Results = data.ProjectTo<AdvertistmentPositionViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }


        public AdvertistmentPositionViewModel GetById(string id)
        {
            return Mapper.Map<AdvertistmentPosition, AdvertistmentPositionViewModel>(_advertistmentPositionRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(AdvertistmentPositionViewModel ads)
        {
            var page = Mapper.Map<AdvertistmentPositionViewModel, AdvertistmentPosition>(ads);
            _advertistmentPositionRepository.Update(page);
        }

        public List<AdvertistmentPositionViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _advertistmentPositionRepository.FindAll(x => x.Name.Contains(keyword)).ProjectTo<AdvertistmentPositionViewModel>()
                : _advertistmentPositionRepository.FindAll().ProjectTo<AdvertistmentPositionViewModel>();
            return query.ToList();
        }


        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
