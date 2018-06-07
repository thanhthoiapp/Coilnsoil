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
    public class AdvertistmentService : IAdvertistmentService
    {
        IRepository<Advertistment, int> _advertistmentRepository;
        IRepository<AdvertistmentPosition, string> _advertistmentPositionRepository;
        IUnitOfWork _unitOfWork;

        public AdvertistmentService(IRepository<Advertistment, int> advertistmentRepository, IRepository<AdvertistmentPosition, string> advertistmentPositionRepository,
            IUnitOfWork unitOfWork)
        {
            _advertistmentRepository = advertistmentRepository;
            _advertistmentPositionRepository = advertistmentPositionRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Add(AdvertistmentViewModel pageVm)
        {
            var page = Mapper.Map<AdvertistmentViewModel, Advertistment>(pageVm);
            _advertistmentRepository.Add(page);
        }

        public void Delete(int id)
        {
            _advertistmentRepository.Remove(id);
        }

        public List<AdvertistmentViewModel> GetAll()
        {
            return _advertistmentRepository.FindAll(c => c.PositionId)
                .ProjectTo<AdvertistmentViewModel>().ToList();
        }

        public List<AdvertistmentPositionViewModel> GetAllPosition()
        {
            return _advertistmentPositionRepository.FindAll().OrderBy(x => x.Id).ProjectTo<AdvertistmentPositionViewModel>().ToList();
        }

        //public PagedResult<AdvertistmentViewModel> GetAllPaging(string keyword, int pageSize, int page = 1)
        //{
        //    var query = _advertistmentRepository.FindAll();
        //    if (!string.IsNullOrEmpty(keyword))
        //        query = query.Where(x => x.Name.Contains(keyword));

        //    int totalRow = query.Count();
        //    var data = query.OrderByDescending(x => x.Id)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize);

        //    var paginationSet = new PagedResult<AdvertistmentViewModel>()
        //    {
        //        Results = data.ProjectTo<AdvertistmentViewModel>().ToList(),
        //        CurrentPage = page,
        //        RowCount = totalRow,
        //        PageSize = pageSize,
        //    };

        //    return paginationSet;
        //}
        public PagedResult<AdvertistmentViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _advertistmentRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<AdvertistmentViewModel>()
            {
                Results = data.ProjectTo<AdvertistmentViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }


        public AdvertistmentViewModel GetById(int id)
        {
            return Mapper.Map<Advertistment, AdvertistmentViewModel>(_advertistmentRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(AdvertistmentViewModel ads)
        {
            var page = Mapper.Map<AdvertistmentViewModel, Advertistment>(ads);
            _advertistmentRepository.Update(page);
        }

        public List<AdvertistmentViewModel> GetLastest(int top)
        {
            return _advertistmentRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<AdvertistmentViewModel>().ToList();
        }

        //public List<AdvertistmentViewModel> GetHotBlog(int top)
        //{
        //    return _advertistmentRepository.FindAll(x => x.Status == Status.Active && x.HotFlag == true)
        //        .OrderByDescending(x => x.DateCreated)
        //        .Take(top)
        //        .ProjectTo<AdvertistmentViewModel>()
        //        .ToList();
        //}

        //public List<AdvertistmentViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        //{
        //    var query = _advertistmentRepository.FindAll(x => x.Status == Status.Active);

        //    switch (sort)
        //    {
        //        case "popular":
        //            query = query.OrderByDescending(x => x.ViewCount);
        //            break;

        //        default:
        //            query = query.OrderByDescending(x => x.DateCreated);
        //            break;
        //    }

        //    totalRow = query.Count();

        //    return query.Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ProjectTo<AdvertistmentViewModel>().ToList();
        //}

        public List<string> GetListByName(string name)
        {
            return _advertistmentRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }

        //public List<AdvertistmentViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        //{
        //    var query = _advertistmentRepository.FindAll(x => x.Status == Status.Active
        //    && x.Name.Contains(keyword));

        //    switch (sort)
        //    {
        //        case "popular":
        //            query = query.OrderByDescending(x => x.ViewCount);
        //            break;

        //        default:
        //            query = query.OrderByDescending(x => x.DateCreated);
        //            break;
        //    }

        //    totalRow = query.Count();

        //    return query.Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ProjectTo<AdvertistmentViewModel>()
        //        .ToList();
        //}

        public List<AdvertistmentViewModel> GetReatedBlogs(int id, int top)
        {
            return _advertistmentRepository.FindAll(x => x.Status == Status.Active
                && x.Id != id)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<AdvertistmentViewModel>()
            .ToList();
        }
        //public List<AdvertistmentViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        //{
            //var query = from p in _advertistmentRepository.FindAll()
            //            join pt in _blogTagRepository.FindAll()
            //            on p.Id equals pt.BlogId
            //            where pt.TagId == tagId && p.Status == Status.Active
            //            orderby p.DateCreated descending
            //            select p;

            //totalRow = query.Count();

            //query = query.Skip((page - 1) * pageSize).Take(pageSize);

            //var model = query
            //    .ProjectTo<AdvertistmentViewModel>();
            //return model.ToList();
        //}

        //public TagViewModel GetTag(string tagId)
        //{
        //    return Mapper.Map<Tag, TagViewModel>(_tagRepository.FindSingle(x => x.Id == tagId));
        //}

        public List<AdvertistmentViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _advertistmentRepository.FindAll(x => x.Name.Contains(keyword)).ProjectTo<AdvertistmentViewModel>()
                : _advertistmentRepository.FindAll().ProjectTo<AdvertistmentViewModel>();
            return query.ToList();
        }

        //public List<TagViewModel> GetListTag(string searchText)
        //{
        //    return _tagRepository.FindAll(x => x.Type == CommonConstants.ProductTag
        //    && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        //}

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
