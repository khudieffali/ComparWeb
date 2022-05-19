using Core.Entity;

namespace Entities.Concrete
{
    public class CourseToSkill : IEntity
       {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SkillId { get; set; }
        public virtual Skill? Skill { get; set; }

    }
}
