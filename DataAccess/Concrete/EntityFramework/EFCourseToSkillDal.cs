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
    public class EFCourseToSkillDal : EFEntityRepositoryBase<CourseToSkill, ComparDbContext>, ICourseToSkillDal
    {
        public async Task DeleteRange(List<CourseToSkill> courseToSkills)
        {
            using ComparDbContext context = new();
            context.CourseToSkills.RemoveRange(courseToSkills);
            await context.SaveChangesAsync();
        }

        public async Task<CourseToSkill> GetByIdCourseToSkill(Expression<Func<CourseToSkill, bool>>? filters)
        {
            using ComparDbContext context = new();
            var course = context.CourseToSkills
                .Include(c => c.Skill)
                .FirstOrDefaultAsync(filters);
            return await course;
        }

        public async Task<List<CourseToSkill>> GetCourseToSkill(Expression<Func<CourseToSkill, bool>>? filters)
        {
            using ComparDbContext context = new();
            var courses = context.CourseToSkills
                .Include(c => c.Skill)
                 .AsQueryable();

            if (filters != null)
            {
                courses = courses.Where(filters);
            }
            return await courses.ToListAsync();
        }
    }
}
