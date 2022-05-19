using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IVideoDal : IEntityRepository<Video>
    {
        Task<List<Video>> GetAllInclude(Expression<Func<Video, bool>>? filters);
        Task<Video> GetByIdInclude(Expression<Func<Video, bool>>? filters);
    }
}
