using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Picture;
using ThanhThoiApp.Application.ViewModels.Product;
using ThanhThoiApp.Application.ViewModels.System;
using ThanhThoiApp.Data.Entities;

namespace ThanhThoiApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Description, c.ParentId, c.HomeOrder, c.Image, c.HomeFlag,
                c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription, c.Type, c.Path));

            CreateMap<ProductViewModel, Product>()
           .ConstructUsing(c => new Product(c.Name, c.CategoryId, c.Image, c.Price, c.OriginalPrice,
           c.PromotionPrice, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Unit, c.Status,
           c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<PictureViewModel, Picture>()
           .ConstructUsing(c => new Picture(c.Name, c.CategoryId, c.Image,
           c.Description, c.Content, c.HomeFlag, c.Status,
           c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<AppUserViewModel, AppUser>()
            .ConstructUsing(c => new AppUser(c.Id.GetValueOrDefault(Guid.Empty), c.FullName, c.UserName, 
            c.Email, c.PhoneNumber, c.Avatar, c.Status));

            CreateMap<PermissionViewModel, Permission>()
            .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));



            CreateMap<BillViewModel, Bill>()
              .ConstructUsing(c => new Bill(c.Id,c.CustomerName, c.CustomerAddress, 
              c.CustomerMobile, c.CustomerMessage, c.BillStatus, 
              c.PaymentMethod, c.Status, c.CustomerId));

            CreateMap<BillDetailViewModel, BillDetail>()
              .ConstructUsing(c => new BillDetail(c.Id, c.BillId, c.ProductId,
              c.Quantity, c.Price, c.ColorId, c.SizeId));


            CreateMap<ContactViewModel, Contact>()
                .ConstructUsing(c => new Contact(c.Id, c.Name, c.Phone, c.Email, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Status));

            CreateMap<FeedbackViewModel, Feedback>()
                .ConstructUsing(c => new Feedback(c.Id, c.Name, c.Email, c.Message, c.Status));

            CreateMap<PageViewModel, Page>()
             .ConstructUsing(c => new Page(c.Id, c.Name, c.Alias, c.Content, c.Status));

            CreateMap<FooterViewModel, Footer>()
             .ConstructUsing(c => new Footer(c.Id, c.Content));

            CreateMap<SystemConfigViewModel, SystemConfig>()
             .ConstructUsing(c => new SystemConfig(c.Id, c.Name, c.Status, c.Value1, c.Value2, c.Value3, c.Value4, c.Value5));

            CreateMap<AnnouncementViewModel, Announcement>()
            .ConstructUsing(c => new Announcement(c.Content, c.Title, c.Status, c.UserId));

            CreateMap<FunctionViewModel, Function>()
             .ConstructUsing(c => new Function(c.Name, c.IconCss, c.ParentId, c.SortOrder, c.Status, c.URL));
            CreateMap<BlogViewModel, Blog>()
            .ConstructUsing(c => new Blog(c.Name, c.CategoryId, c.Content, c.Description, c.HomeFlag, c.HotFlag, c.Image, c.SeoAlias, c.SeoDescription, c.SeoKeywords, c.SeoPageTitle, c.Status, c.Tags, c.ViewCount, c.PostById));
            //CreateMap<BlogTagViewModel, BlogTag>()
            // .ConstructUsing(c => new BlogTag(c.Id, c.BlogId, c.TagId));
            // CreateMap<TagViewModel, Tag>()
            //.ConstructUsing(c => new Tag(c.Name, c.Type));

            CreateMap<SlideViewModel, Slide>()
             .ConstructUsing(c => new Slide(c.Name, c.Description, c.Image, c.Url, c.DisplayOrder, c.Status, c.Content, c.GroupAlias));

            CreateMap<AdvertistmentViewModel, Advertistment>()
            .ConstructUsing(c => new Advertistment(c.Id, c.Name, c.Description, c.Image, c.Url, c.SortOrder, c.PositionId, c.Status));

            CreateMap<AdvertistmentPositionViewModel, AdvertistmentPosition>()
            .ConstructUsing(c => new AdvertistmentPosition(c.Id, c.Name, c.PageId));

        }
    }
}
