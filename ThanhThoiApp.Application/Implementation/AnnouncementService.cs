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
    public class AnnouncementService : IAnnouncementService
    {
        IRepository<Announcement, string> _announcementRepository;
        IUnitOfWork _unitOfWork;

        public AnnouncementService(IRepository<Announcement, string> announcementRepository,
            IUnitOfWork unitOfWork)
        {
            _announcementRepository = announcementRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Add(AnnouncementViewModel pageVm)
        {
            var page = Mapper.Map<AnnouncementViewModel, Announcement>(pageVm);
            _announcementRepository.Add(page);
        }

        public void Delete(string id)
        {
            _announcementRepository.Remove(id);
        }

        public List<AnnouncementViewModel> GetAll()
        {
            return _announcementRepository.FindAll(c => c.Title)
                .ProjectTo<AnnouncementViewModel>().ToList();
        }

        public PagedResult<AnnouncementViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _announcementRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Id.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<AnnouncementViewModel>()
            {
                Results = data.ProjectTo<AnnouncementViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public AnnouncementViewModel GetById(string id)
        {
            return Mapper.Map<Announcement, AnnouncementViewModel>(_announcementRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(AnnouncementViewModel blog)
        {
            var page = Mapper.Map<AnnouncementViewModel, Announcement>(blog);
            _announcementRepository.Update(page);
        }



        public List<AnnouncementViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = _announcementRepository.FindAll(x => x.Status == Status.Active);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.Title);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AnnouncementViewModel>().ToList();
        }

        public List<string> GetListByName(string name)
        {
            return _announcementRepository.FindAll(x => x.Status == Status.Active
            && x.Title.Contains(name)).Select(y => y.Title).ToList();
        }

        public List<AnnouncementViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _announcementRepository.FindAll(x => x.Status == Status.Active
            && x.Title.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.Title);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AnnouncementViewModel>()
                .ToList();
        }


        public List<AnnouncementViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _announcementRepository.FindAll(x => x.Title.Contains(keyword)).ProjectTo<AnnouncementViewModel>()
                : _announcementRepository.FindAll().ProjectTo<AnnouncementViewModel>();
            return query.ToList();
        }


        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

    }
}
