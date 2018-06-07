using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.IRepositories;

namespace ThanhThoiApp.Data.EF.Repositories
{
    public class AdvertistmentRepository : EFRepository<Advertistment, int>, IAdvertistmentRepository
    {
        public AdvertistmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
