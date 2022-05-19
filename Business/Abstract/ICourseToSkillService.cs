using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseToSkillService
    {
        Task<List<CourseToSkill>> GetCourseToSkill(int[]? ids,int? courseId);
        Task<List<CourseToSkill>> GetCourseToSkillDelete(int[]? ids, int? courseId);

        Task<CourseToSkill> GetByIdCourseToSkill(int? id,int? courseId);

        Task DeleteCourseSkill(List<CourseToSkill> courseToSkill);

    }
}
