using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICourseDal : IEntityRepository<Course>
    {
        Task<List<Course>> GetAllInclude(Expression<Func<Course, bool>>? filters);
        Task<Course> GetByIdInclude(Expression<Func<Course, bool>>? filters);
        Task AddMyCourse(Course course);
        Task  UpdateMyCourse(Course course);


    }
}
