using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IArticleImageService
    {
        Task AddArticleImage(ArticleImage articleImage);
        Task DeleteArticleImages(List<ArticleImage> articleImages);
        Task<List<ArticleImage>> GetByIdArticleImages(List<int> ids, int? articleId);


    }
}
