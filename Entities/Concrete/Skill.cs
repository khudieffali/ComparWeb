using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Skill : IEntity
       {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string MainPhoto { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; }
    }
}
