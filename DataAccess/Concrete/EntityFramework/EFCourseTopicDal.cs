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
    public class EFCourseTopicDal : EFEntityRepositoryBase<CourseTopic, ComparDbContext>, ICourseTopicDal
    {
        public async Task<List<CourseTopic>> GetAllInclude(Expression<Func<CourseTopic, bool>>? filters)
        {
            using ComparDbContext context = new();
            var courseTopic= context.CourseTopics
                .Where(x=>!x.IsDeleted)
                .Include(x=>x.Course)
                .AsQueryable();
            if (filters != null)
            {
                courseTopic = courseTopic.Where(filters);
            }
            return await courseTopic.ToListAsync();
        }

        public async Task<CourseTopic> GetByIdInclude(Expression<Func<CourseTopic, bool>>? filters)
        {
            using ComparDbContext context = new();
            var courseTopic = context.CourseTopics
                .Where(c => !c.IsDeleted)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(filters);
            return await courseTopic;
        }
    }
}
