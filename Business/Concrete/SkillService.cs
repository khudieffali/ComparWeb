using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SkillService : ISkillService
    {
        private readonly ISkillDal _dal;

        public SkillService(ISkillDal dal)
        {
            _dal = dal;
        }

        public async Task AddSkill(Skill skill)
        {
          _dal.Add(skill);
        }

        public async Task<Skill> GetByIdSkill(int? id)
        {
           return _dal.Get(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Skill>> GetSkills()
        {
            return _dal.GetAll(x=>!x.IsDeleted);
        }

        public async Task UpdateSkill(Skill skill)
        {
           _dal.Update(skill);
        }
    }
}
