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
    public class CourseService : ICourseService
    {
        private readonly ICourseDal _dal;

        public CourseService(ICourseDal dal)
        {
            _dal = dal;
        }

        public async Task AddCourse(Course course)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            course.CreatedDate = custDateTime;
            course.UpdatedDate=custDateTime;
            await _dal.AddMyCourse(course);
        }

        public async Task<Course> GetByIdCourses(int? id)
        {
            return await _dal.GetByIdInclude(x => !x.IsDeleted && x.Id==id);
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _dal.GetAllInclude(x=>!x.IsDeleted);
        }

        public async Task UpdateCourse(Course course)
        {

            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            course.UpdatedDate = custDateTime;
            _dal.UpdateMyCourse(course);
        }
    }
}
