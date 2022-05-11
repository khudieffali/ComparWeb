using Core.Entity;

namespace Entities.Concrete
{
    public class Article : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public string MainPhoto { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool InActive { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public virtual List<Video> Video { get; set; } = null!;
        public virtual List<ArticleImage> ArticleImages { get; set; } = null!;

    }
}
