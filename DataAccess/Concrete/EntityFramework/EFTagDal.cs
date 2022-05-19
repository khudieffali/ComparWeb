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
    public class EFTagDal : EFEntityRepositoryBase<Tag, ComparDbContext>, ITagDal
    {
        public async Task<List<Tag>> GetAllInclude(Expression<Func<Tag, bool>>? filters)
        {
            using ComparDbContext context = new();
            var tags = context.Tags
                .Where(c => !c.IsDeleted)
                .AsQueryable();

            if (filters != null)
            {
                tags = tags.Where(filters);
            }
            return await tags.ToListAsync();
        }

        public async Task<Tag> GetByIdInclude(Expression<Func<Tag, bool>>? filters)
        {
            using ComparDbContext context = new();
            var tag = context.Tags
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(filters);
            return await tag;
        }
    }
}
