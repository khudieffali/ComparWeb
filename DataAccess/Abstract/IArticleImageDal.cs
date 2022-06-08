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
    public interface IArticleImageDal :IEntityRepository<ArticleImage>
    {
        Task DeleteRange(List<ArticleImage> articleImages);
        Task<List<ArticleImage>> GetByIdArticleImages(Expression<Func<ArticleImage, bool>>? filters);

    }
}
