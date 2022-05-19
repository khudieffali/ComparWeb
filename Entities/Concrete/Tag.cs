using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
