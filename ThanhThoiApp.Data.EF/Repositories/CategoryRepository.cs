using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ThanhThoiApp.Data.Entities;
using ThanhThoiApp.Data.IRepositories;

namespace ThanhThoiApp.Data.EF.Repositories
{
    public class CategoryRepository : EFRepository<Category, int>, ICategoryRepository
    {
        AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Category> GetByAlias(string alias)
        {
            return _context.Category.Where(x => x.SeoAlias == alias).ToList();
        }
    }
}
