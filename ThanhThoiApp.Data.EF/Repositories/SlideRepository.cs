using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.IRepositories;

namespace ThanhThoiApp.Data.EF.Repositories
{
    public class SlideRepository : EFRepository<Slide, int>, ISlideRepository
    {
        public SlideRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
