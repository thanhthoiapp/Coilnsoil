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
    public class SlideService : ISlideService
    {
        IRepository<Slide, int> _slideRepository;
        IUnitOfWork _unitOfWork;

        public SlideService(IRepository<Slide, int> blogRepository,
            IUnitOfWork unitOfWork)
        {
            _slideRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Add(SlideViewModel slideVm)
        {
            var page = Mapper.Map<SlideViewModel, Slide>(slideVm);
            _slideRepository.Add(page);
        }

        public void Delete(int id)
        {
            _slideRepository.Remove(id);
        }

        public List<SlideViewModel> GetAll()
        {
            return _slideRepository.FindAll(c => c.GroupAlias)
                .ProjectTo<SlideViewModel>().ToList();
        }

        //public PagedResult<SlideViewModel> GetAllPaging(string keyword, int pageSize, int page = 1)
        //{
        //    var query = _slideRepository.FindAll();
        //    if (!string.IsNullOrEmpty(keyword))
        //        query = query.Where(x => x.Name.Contains(keyword));

        //    int totalRow = query.Count();
        //    var data = query.OrderByDescending(x => x.Id)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize);

        //    var paginationSet = new PagedResult<SlideViewModel>()
        //    {
        //        Results = data.ProjectTo<SlideViewModel>().ToList(),
        //        CurrentPage = page,
        //        RowCount = totalRow,
        //        PageSize = pageSize,
        //    };

        //    return paginationSet;
        //}
        public PagedResult<SlideViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _slideRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.GroupAlias)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<SlideViewModel>()
            {
                Results = data.ProjectTo<SlideViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }


        public void Update(SlideViewModel slideVm)
        {
            var slide = Mapper.Map<SlideViewModel, Slide>(slideVm);
            _slideRepository.Update(slide);
        }




        public SlideViewModel GetById(int id)
        {
            return Mapper.Map<Slide, SlideViewModel>(_slideRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

       
        public List<string> GetListByName(string name)
        {
            return _slideRepository.FindAll(x => x.Status == true
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }


        public List<SlideViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _slideRepository.FindAll(x => x.Name.Contains(keyword)).ProjectTo<SlideViewModel>()
                : _slideRepository.FindAll().ProjectTo<SlideViewModel>();
            return query.ToList();
        }


        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public List<SlideViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public List<SlideViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            throw new NotImplementedException();
        }
    }
}
