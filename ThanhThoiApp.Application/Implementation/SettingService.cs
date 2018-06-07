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
    public class SettingService : ISettingService
    {
        private IRepository<SystemConfig, String> _systemConfigRepository;
        private IUnitOfWork _unitOfWork;

        public SettingService(IRepository<SystemConfig, string> systemConfigRepository,
            IUnitOfWork unitOfWork)
        {
            _systemConfigRepository = systemConfigRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(SystemConfigViewModel SettingVm)
        {
            var page = Mapper.Map<SystemConfigViewModel, SystemConfig>(SettingVm);
            _systemConfigRepository.Add(page);
        }

        public void Delete(string id)
        {
            _systemConfigRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SystemConfigViewModel> GetAll()
        {
            return _systemConfigRepository.FindAll().ProjectTo<SystemConfigViewModel>().ToList();
        }

        public PagedResult<SystemConfigViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _systemConfigRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Id.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<SystemConfigViewModel>()
            {
                Results = data.ProjectTo<SystemConfigViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public SystemConfigViewModel GetById(string id)
        {
            return Mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SystemConfigViewModel SettingVm)
        {
            var page = Mapper.Map<SystemConfigViewModel, SystemConfig>(SettingVm);
            _systemConfigRepository.Update(page);
        }
    }
}
