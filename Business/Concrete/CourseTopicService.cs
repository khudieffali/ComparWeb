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
    public class CourseTopicService : ICourseTopicService
    {
        private readonly ICourseTopicDal _dal;

        public CourseTopicService(ICourseTopicDal dal)
        {
            _dal = dal;
        }

        public async Task AddCourseTopic(CourseTopic courseTopic)
        {
              _dal.Add(courseTopic);
        }

        public async Task<CourseTopic> GetByIdCourseTopics(int? id)
        {
           return await _dal.GetByIdInclude(x=>x.Id==id);
        }

        public async Task<List<CourseTopic>> GetCourseTopics()
        {
           return await _dal.GetAllInclude(x=>!x.IsDeleted);
        }

        public async Task UpdateCourseTopic(CourseTopic courseTopic)
        {
           _dal.Update(courseTopic);
        }
    }
}
