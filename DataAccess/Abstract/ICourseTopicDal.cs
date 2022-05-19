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
    public interface ICourseTopicDal:IEntityRepository<CourseTopic>
    {
        Task<List<CourseTopic>> GetAllInclude(Expression<Func<CourseTopic, bool>>? filters);
        Task<CourseTopic> GetByIdInclude(Expression<Func<CourseTopic, bool>>? filters);
    }
}
