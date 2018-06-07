using System.Collections.Generic;
using ThanhThoiApp.Application.ViewModels.Blog;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Picture;
using ThanhThoiApp.Application.ViewModels.Product;

namespace ThanhThoiApp.Models
{
    public class HomeViewModel
    {
        public List<BlogViewModel> LastestBlogs { get; set; }
        public List<SlideViewModel> HomeSlides { get; set; }
        public List<SlideViewModel> HomeBrand { get; set; }
        public List<ProductViewModel> HotProducts { get; set; }
        public List<ProductViewModel> TopSellProducts { get; set; }

        public List<BlogViewModel> TopService { get; set; }
        public List<BlogViewModel> TopNews { get; set; }
        public List<PictureViewModel> TopProject { get; set; }
        public List<PictureDetailViewModel> TopProjectPhoto { get; set; }
        public FooterViewModel Footer { set; get; }
        public FooterViewModel IntroFooterId { set; get; }
        public List<CategoryViewModel> HomeCategory { set; get; }
        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}
