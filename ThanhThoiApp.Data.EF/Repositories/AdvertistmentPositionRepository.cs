using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.IRepositories;

namespace ThanhThoiApp.Data.EF.Repositories
{
    public class AdvertistmentPositionRepository : EFRepository<AdvertistmentPosition, string>, IAdvertistmentPositionRepository
    {
        public AdvertistmentPositionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
