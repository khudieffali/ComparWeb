using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Article : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Slug { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        [Required,Range(120,150)]
        public string MetaDescription { get; set; } = null!;
        [Required]
        public string MainPhoto { get; set; } = null!;
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public bool InActive { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public virtual List<Video> Video { get; set; } = null!;
        public virtual List<ArticleImage> ArticleImages { get; set; } = null!;

    }
}
