using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITagService
    {
        Task<List<Tag>> GetTags();
        Task<Tag> GetTagById(int? id);
        Task Add(Tag tag);
        Task Update(Tag tag);
    }
}
