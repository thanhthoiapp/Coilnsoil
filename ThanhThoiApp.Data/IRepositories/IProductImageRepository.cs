using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Infrastructure.Interfaces;

namespace ThanhThoiApp.Data.IRepositories
{
    public interface IProductImageRepository : IRepository<ProductImage, int>
    {
    }
}
