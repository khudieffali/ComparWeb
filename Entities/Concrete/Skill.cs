using Core.Entity;

namespace Entities.Concrete
{
    public class Skill : IEntity
       {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string MainPhoto { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
