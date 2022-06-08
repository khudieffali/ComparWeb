using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDal : EFEntityRepositoryBase<Category, ComparDbContext>, ICategoryDal
    {
        public async Task<List<Category>> GetAllInclude(Expression<Func<Category, bool>>? filters)
        {
            using ComparDbContext context = new();
            var categories = context.Categories
                .Where(c => !c.IsDeleted)
                .Include(c =>c.Articles)
                .AsQueryable();

            if (filters != null)
            {
                categories = categories.Where(filters);
            }
            return await categories.ToListAsync();
        }

     
    }
}
