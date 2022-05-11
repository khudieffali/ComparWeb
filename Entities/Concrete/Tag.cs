using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; } = null!;
    }
}
