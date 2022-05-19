using Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "varchar")]
        [RegularExpression("^[a-zA-z0-9_]*$", ErrorMessage = "Only Alphabets,Numbers and _ symbol allowed.")]
        public string Slug { get; set; } = null!;
        public string? SlidePhoto { get; set; }
        public string? MainPhoto { get; set; }
        [Required]
        public float OneLessonDuration { get; set; }
        [Required]
        public string WeekSchedule { get; set; } = null!;
        [Required]
        public int TotalDuration { get; set; }
        [Required]
        public int ViewCount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        [Required]
        public bool InActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public virtual List<CourseToSkill>? CourseToSkills { get; set; }
        public virtual List<CourseTopic>? CourseTopics { get; set; }
    }
}
