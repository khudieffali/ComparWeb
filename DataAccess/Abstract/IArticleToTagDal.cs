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
    public interface IArticleToTagDal : IEntityRepository<ArticleToTag>
    {
        Task<List<ArticleToTag>> GetArticleToTags(Expression<Func<ArticleToTag, bool>>? filters);
        Task<ArticleToTag> GetByIdArticleToTags(Expression<Func<ArticleToTag, bool>>? filters);
        Task DeleteRange(List<ArticleToTag> articleTags);
    }
}
