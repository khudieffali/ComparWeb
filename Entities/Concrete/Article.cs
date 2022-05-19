using Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "varchar")]
        [RegularExpression("^[a-zA-z0-9_]*$", ErrorMessage = "Only Alphabets,Numbers and _ symbol allowed.")]
        public string Slug { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        [StringLength(150,MinimumLength =120)]
        public string MetaDescription { get; set; } = null!;
        public string? MainPhoto { get; set; }
        public string? CoverPhoto { get; set; }
        [Required]
        public int ViewCount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public bool InActive { get; set; } = true;
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Video>? Video { get; set; }
        public virtual List<ArticleToTag>? ArticleToTags{ get; set; }
        public virtual List<ArticleImage>? ArticleImages { get; set; }

    }
}
