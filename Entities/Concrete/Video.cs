using Core.Entity;

namespace Entities.Concrete
{
    public class Video : IEntity
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
