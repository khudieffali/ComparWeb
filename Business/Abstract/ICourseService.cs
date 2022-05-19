using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseService
    {
        Task<List<Course>> GetCourses();
        Task<Course> GetByIdCourses(int? id);
        Task AddCourse(Course course);  
        Task UpdateCourse(Course course);
    }
    
}
