using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Infrastructure.Interfaces;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Implementation
{
    public class FooterService : IFooterService
    {
        private IRepository<Footer, string> _footerRepository;
        private IUnitOfWork _unitOfWork;

        public FooterService(IRepository<Footer, string> footerRepository,
            IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(FooterViewModel footerVm)
        {
            var page = Mapper.Map<FooterViewModel, Footer>(footerVm);
            _footerRepository.Add(page);
        }

        public void Delete(string id)
        {
            _footerRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<FooterViewModel> GetAll()
        {
            return _footerRepository.FindAll().ProjectTo<FooterViewModel>().ToList();
        }

        public PagedResult<FooterViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _footerRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Id.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<FooterViewModel>()
            {
                Results = data.ProjectTo<FooterViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public FooterViewModel GetById(string id)
        {
            return Mapper.Map<Footer, FooterViewModel>(_footerRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(FooterViewModel footerVm)
        {
            var page = Mapper.Map<FooterViewModel, Footer>(footerVm);
            _footerRepository.Update(page);
        }
    }
}
