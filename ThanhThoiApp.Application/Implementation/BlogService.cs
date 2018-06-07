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
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog, int> _blogRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<BlogTag, int> _blogTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IRepository<Blog, int> blogRepository,
            IRepository<BlogTag, int> blogTagRepository,
            IRepository<Tag, string> tagRepository,
            IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public BlogViewModel Add(BlogViewModel blogVm)
        {
            List<BlogTag> blogTags = new List<BlogTag>();
            if (!string.IsNullOrEmpty(blogVm.Tags))
            {
                string[] tags = blogVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.BlogTag
                        };
                        _tagRepository.Add(tag);
                    }

                    BlogTag blogTag = new BlogTag
                    {
                        BlogId = blogVm.Id,
                        TagId = tagId
                    };
                    blogTags.Add(blogTag);
                }
                var blog = Mapper.Map<BlogViewModel, Blog>(blogVm);
                foreach (var blogTag in blogTags)
                {
                    blog.BlogTags.Add(blogTag);
                }
                _blogRepository.Add(blog);

            }
            return blogVm;
        }

        public void Delete(int id)
        {
            _blogRepository.Remove(id);
        }

        public List<BlogViewModel> GetAll()
        {
            return _blogRepository.FindAll(c => c.BlogTags)
                .ProjectTo<BlogViewModel>().ToList();
        }

        public List<TagViewModel> GetAllTags()
        {
            return _tagRepository.FindAll()
                .ProjectTo<TagViewModel>().ToList();
        }
        //public PagedResult<BlogViewModel> GetAllPaging(string keyword, int pageSize, int page = 1)
        //{
        //    var query = _blogRepository.FindAll();
        //    if (!string.IsNullOrEmpty(keyword))
        //        query = query.Where(x => x.Name.Contains(keyword));

        //    int totalRow = query.Count();
        //    var data = query.OrderByDescending(x => x.Id)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize);

        //    var paginationSet = new PagedResult<BlogViewModel>()
        //    {
        //        Results = data.ProjectTo<BlogViewModel>().ToList(),
        //        CurrentPage = page,
        //        RowCount = totalRow,
        //        PageSize = pageSize,
        //    };

        //    return paginationSet;
        //}
        public PagedResult<BlogViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _blogRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value || x.Category.ParentId == categoryId.Value);

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<BlogViewModel>()
            {
                Results = data.ProjectTo<BlogViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public PagedResult<BlogViewModel> GetAllPagingTag(int? categoryId, string keyword, int page, int pageSize)
        {
            var tagid = _tagRepository.FindAll();
            var blogid = _blogTagRepository.FindAll();
            var blog = _blogRepository.FindAll();

            var query = from t in tagid
                        join bi in blogid on t.Id equals bi.TagId
                        join b in blog on bi.TagId equals b.Tags

                        where b.CategoryId == categoryId && b.Status == Status.Active
                        orderby b.DateCreated descending
                        select b;

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<BlogViewModel>()
            {
                Results = data.ProjectTo<BlogViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }


        public BlogViewModel GetById(int id)
        {
            return Mapper.Map<Blog, BlogViewModel>(_blogRepository.FindById(id));
        }
        public TagViewModel GetByAlias(string id)
        {
            return Mapper.Map<Tag, TagViewModel>(_tagRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(BlogViewModel blog)
        {
            _blogRepository.Update(Mapper.Map<BlogViewModel, Blog>(blog));
            if (!string.IsNullOrEmpty(blog.Tags))
            {
                string[] tags = blog.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }
                    _blogTagRepository.RemoveMultiple(_blogTagRepository.FindAll(x => x.Id == blog.Id).ToList());
                    BlogTag blogTag = new BlogTag
                    {
                        BlogId = blog.Id,
                        TagId = tagId
                    };
                    _blogTagRepository.Add(blogTag);
                }
            }
        }

        public List<BlogViewModel> GetLastest(int top)
        {
            return _blogRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<BlogViewModel>().ToList();
        }

        public List<BlogViewModel> GetPostByCateId(int id, int top)
        {
            return _blogRepository.FindAll(x => x.Status == Status.Active).Where(x => x.CategoryId == id || x.Category.ParentId == id).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<BlogViewModel>().ToList();
        }

        public List<BlogViewModel> GetHotBlog(int top)
        {
            return _blogRepository.FindAll(x => x.Status == Status.Active && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<BlogViewModel>()
                .ToList();
        }

        public List<BlogViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = _blogRepository.FindAll(x => x.Status == Status.Active);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BlogViewModel>().ToList();
        }

        public List<string> GetListByName(string name)
        {
            return _blogRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }

        public List<BlogViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _blogRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BlogViewModel>()
                .ToList();
        }

        public List<BlogViewModel> GetReatedBlogs(int id, int top)
        {
            return _blogRepository.FindAll(x => x.Status == Status.Active
                && x.Id != id)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<BlogViewModel>()
            .ToList();
        }

        public List<TagViewModel> GetListTagById(int id)
        {
            return _blogTagRepository.FindAll(x => x.BlogId == id, c => c.Tag)
                .Select(y => y.Tag)
                .ProjectTo<TagViewModel>()
                .ToList();
        }

        public void IncreaseView(int id)
        {
            var product = _blogRepository.FindById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
            Save();
        }

        public List<BlogViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _blogRepository.FindAll()
                        join pt in _blogTagRepository.FindAll()
                        on p.Id equals pt.BlogId
                        where pt.TagId == tagId && p.Status == Status.Active
                        orderby p.DateCreated descending
                        select p;

            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var model = query
                .ProjectTo<BlogViewModel>();
            return model.ToList();
        }
        public List<BlogViewModel> GetListPostByTagId(string idtag)
        {
            var tagid = _tagRepository.FindAll();
            var blogid = _blogTagRepository.FindAll();
            var blog = _blogRepository.FindAll();

            var query = from t in tagid 
                        join bi in blogid on t.Id equals bi.TagId
                        join b in blog on bi.BlogId equals b.Id

                        where bi.TagId == idtag && b.Status == Status.Active
                        orderby b.DateCreated descending
                        select b;
            return query.ProjectTo<BlogViewModel>().ToList();
        }
        public TagViewModel GetTag(string tagId)
        {
            return Mapper.Map<Tag, TagViewModel>(_tagRepository.FindSingle(x => x.Id == tagId));
        }

        public List<BlogViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _blogRepository.FindAll(x => x.Name.Contains(keyword)).ProjectTo<BlogViewModel>()
                : _blogRepository.FindAll().ProjectTo<BlogViewModel>();
            return query.ToList();
        }

        public List<TagViewModel> GetListTag(string searchText)
        {
            return _tagRepository.FindAll(x => x.Type == CommonConstants.ProductTag
            && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
