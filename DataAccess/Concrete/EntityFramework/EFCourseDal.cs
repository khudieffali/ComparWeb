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
    public class EFCourseDal : EFEntityRepositoryBase<Course, ComparDbContext>, ICourseDal
    {
        public async Task AddMyCourse(Course course)
        {
            ComparDbContext context = new();
            context.Add(course);
            await context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAllInclude(Expression<Func<Course, bool>>? filters)
        {
            using ComparDbContext context = new();
            var courses = context.Courses
                .Where(c => !c.IsDeleted)
                .Include(c=>c.CourseToSkills)
                .ThenInclude(c=>c.Skill)
                .Include(c=>c.CourseTopics)
                .OrderByDescending(x=>x.CreatedDate)
                .AsQueryable();

            if (filters != null)
            {
                courses = courses.Where(filters);
            }
            return await courses.ToListAsync();
        }
        public async Task<List<Course>> GetAllHomeInclude(Expression<Func<Course, bool>>? filters)
        {
            using ComparDbContext context = new();
            var courses = context.Courses
                .Where(c => !c.IsDeleted && c.InActive)
                .Include(c => c.CourseToSkills)
                .ThenInclude(c => c.Skill)
                .Include(c => c.CourseTopics)
                .OrderByDescending(x => x.CreatedDate)
                .Take(6)
                .AsQueryable();

            if (filters != null)
            {
                courses = courses.Where(filters);
            }
            return await courses.ToListAsync();
        }

        public async Task<Course> GetByIdInclude(Expression<Func<Course, bool>>? filters)
        {
            using ComparDbContext context = new();
            var course = context.Courses
                .Where(c => !c.IsDeleted)
                .Include(c => c.CourseToSkills)
                .ThenInclude(c => c.Skill)
                .Include(c => c.CourseTopics)
                .FirstOrDefaultAsync(filters);
            return  await course;
        }

        public async Task UpdateMyCourse(Course course)
        {
            ComparDbContext context = new();
            context.Update(course);
            await context.SaveChangesAsync(); 
        }
    }
}
