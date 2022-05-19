using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Video : IEntity
    {
        public int Id { get; set; }
        public string? VideoUrl { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CourseTopicId { get; set; }
        public virtual CourseTopic? CourseTopic { get; set; }
    }
}
