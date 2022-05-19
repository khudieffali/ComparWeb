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
    public class TagService : ITagService
    {
        private readonly ITagDal _dal;

        public TagService(ITagDal dal)
        {
            _dal = dal;
        }

        public async Task Add(Tag tag)
        {
           _dal.Add(tag);
        }

        public async Task<Tag> GetTagById(int? id)
        {
           return await _dal.GetByIdInclude(x=>x.Id==id);
        }

        public async Task<List<Tag>> GetTags()
        {
            return await _dal.GetAllInclude(x => !x.IsDeleted);
        }

        public async Task Update(Tag tag)
        {
            _dal.Update(tag); 
        }
    }
}
