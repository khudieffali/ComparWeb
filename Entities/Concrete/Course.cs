using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string MetaDescription { get; set; } = null!;
        [Required]
        public string Slug { get; set; } = null!;
        [Required]
        public string SlidePhoto { get; set; } = null!;
        [Required]
        public string MainPhoto { get; set; } = null!;
        [Required]
        public float OneLessonDuration { get; set; }
        [Required]
        public string WeekSchedule { get; set; } = null!;
        [Required]
        public int TotalDuration { get; set; }
        [Required]
        public bool InActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<CourseTopic>? CourseTopics { get; set; }
    }
}
