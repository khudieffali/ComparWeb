using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CourseToSkillService :ICourseToSkillService
    {
        private readonly ICourseToSkillDal _dal;

        public CourseToSkillService(ICourseToSkillDal dal)
        {
            _dal = dal;
        }

      
        public async Task DeleteCourseSkill(List<CourseToSkill> courseToSkill)
        {
           _dal.DeleteRange(courseToSkill);
        }

        public Task<CourseToSkill> GetByIdCourseToSkill(int? id,int? courseId)
        {
            return _dal.GetByIdCourseToSkill(x => x.SkillId != id && x.CourseId==courseId);
        }

        public async Task<List<CourseToSkill>> GetCourseToSkill(int[]? ids,int? courseId)
        {
            return await _dal.GetCourseToSkill(x=> ids.Contains(x.SkillId) && x.CourseId==courseId);
        }
        public async Task<List<CourseToSkill>> GetCourseToSkillDelete(int[]? ids, int? courseId)
        {
            return await _dal.GetCourseToSkill(x => !ids.Contains(x.SkillId) && x.CourseId == courseId);
        }
    }
}
