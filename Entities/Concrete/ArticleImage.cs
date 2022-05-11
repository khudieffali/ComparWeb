using Core.Entity;

namespace Entities.Concrete
{
    public class ArticleImage : IEntity
    {
        public int Id { get; set; }
        public string ArticleImg { get; set; } = null!;
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; } = null!;

    }
}
