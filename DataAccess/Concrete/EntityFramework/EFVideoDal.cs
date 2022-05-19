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
    public class EFVideoDal : EFEntityRepositoryBase<Video, ComparDbContext>, IVideoDal
    {
        public async Task<List<Video>> GetAllInclude(Expression<Func<Video, bool>>? filters)
        {
            using ComparDbContext context = new();
            var videos = context.Videos
                .Where(c => !c.IsDeleted)
                .Include(c => c.CourseTopic)
                .AsQueryable();

            if (filters != null)
            {
                videos = videos.Where(filters);
            }
            return await videos.ToListAsync();
        }

        public async Task<Video> GetByIdInclude(Expression<Func<Video, bool>>? filters)
        {
            using ComparDbContext context = new();
            var video = context.Videos
                .Where(c => !c.IsDeleted)
                .Include(c => c.CourseTopic)
                .FirstOrDefaultAsync(filters);
            return await video;
        }
    }
}
