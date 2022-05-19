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
    public interface IArticleDal:IEntityRepository<Article>
    {
        Task<List<Article>> GetAllInclude(Expression<Func<Article, bool>>? filters);
        Task<Article> GetByIdInclude(Expression<Func<Article, bool>>? filters);
        Task AddMyArticle(Article article);   
    }
}
