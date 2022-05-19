using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class ArticleImage : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string ArticleImg { get; set; } = null!;
        public int? ArticleId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Article? Article { get; set; }

    }
}
