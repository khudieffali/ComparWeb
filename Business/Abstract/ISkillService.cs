using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISkillService
    {
        Task<List<Skill>> GetSkills();
        Task<Skill> GetByIdSkill(int? id);
        Task AddSkill(Skill skill);
        Task UpdateSkill(Skill skill);
    }
}
