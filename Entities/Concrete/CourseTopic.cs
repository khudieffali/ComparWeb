using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class CourseTopic : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual List<Video>? Videos { get; set; }
    }
}
