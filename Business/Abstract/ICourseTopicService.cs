using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseTopicService
    {
        Task<List<CourseTopic>> GetCourseTopics();
        Task<CourseTopic> GetByIdCourseTopics(int? id);
        Task AddCourseTopic(CourseTopic courseTopic);
        Task UpdateCourseTopic(CourseTopic courseTopic);
    }
}
