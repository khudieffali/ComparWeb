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
    public interface ICourseToSkillDal:IEntityRepository<CourseToSkill>
    {
        Task<List<CourseToSkill>> GetCourseToSkill(Expression<Func<CourseToSkill, bool>>? filters);
        Task<CourseToSkill> GetByIdCourseToSkill(Expression<Func<CourseToSkill, bool>>? filters);
        Task DeleteRange(List<CourseToSkill> courseToSkills);

    }
}
