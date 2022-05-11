using Core.Entity;

namespace Entities.Concrete
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string SlidePhoto { get; set; } = null!;
        public string MainPhoto { get; set; } = null!;
        public float OneLessonDuration { get; set; }
        public string WeekSchedule { get; set; } = null!;
        public int TotalDuration { get; set; }
        public bool InActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<CourseTopic>? CourseTopics { get; set; }
    }
}
