using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Common;
using ThanhThoiApp.Application.ViewModels.Picture;
using ThanhThoiApp.Application.ViewModels.Product;
using ThanhThoiApp.Utilities.Dtos;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface IPictureService : IDisposable
    {
        List<PictureViewModel> GetAll();

        PagedResult<PictureViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);

        PictureViewModel Add(PictureViewModel product);

        void Update(PictureViewModel product);

        void Delete(int id);

        PictureViewModel GetById(int id);

        void Save();

        void AddImages(int productId, string[] images);

        List<PictureDetailViewModel> GetImages(int pictureId);
        List<PictureViewModel> GetPostByCateId(int id, int top);
        List<PictureDetailViewModel> GetPictureLatest(int top);

        List<PictureViewModel> GetLastest(int top);

        List<PictureViewModel> GetHotPicture(int top);
        List<PictureViewModel> GetRelatedPicture(int id, int top);  
    }
}
