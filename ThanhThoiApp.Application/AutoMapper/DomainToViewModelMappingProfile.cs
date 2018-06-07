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
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<Function, FunctionViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Bill, BillViewModel>();
            CreateMap<BillDetail, BillDetailViewModel>();
            CreateMap<Color, ColorViewModel>();
            CreateMap<Size, SizeViewModel>();
            CreateMap<ProductQuantity, ProductQuantityViewModel>().MaxDepth(2);
            CreateMap<ProductImage, ProductImageViewModel>().MaxDepth(2);
            CreateMap<WholePrice, WholePriceViewModel>().MaxDepth(2);
            CreateMap<Advertistment, AdvertistmentViewModel>().MaxDepth(2);
            CreateMap<Announcement, AnnouncementViewModel>().MaxDepth(2);
            CreateMap<AdvertistmentPosition, AdvertistmentPositionViewModel>().MaxDepth(2);
            CreateMap<AdvertistmentPage, AdvertistmentPageViewModel>().MaxDepth(2);
            CreateMap<Blog, BlogViewModel>().MaxDepth(2);
            CreateMap<Picture, PictureViewModel>().MaxDepth(2);
            CreateMap<PictureDetail, PictureDetailViewModel>().MaxDepth(2);
            CreateMap<BlogTag, BlogTagViewModel>().MaxDepth(2);
            CreateMap<Slide, SlideViewModel>().MaxDepth(2);
            CreateMap<SystemConfig, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);

            CreateMap<Feedback, FeedbackViewModel>().MaxDepth(2);
            CreateMap<Contact, ContactViewModel>().MaxDepth(2);
            CreateMap<Page, PageViewModel>().MaxDepth(2);
            CreateMap<Tag, TagViewModel>().MaxDepth(2);
        }
    }
}
