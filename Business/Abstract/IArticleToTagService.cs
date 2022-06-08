using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IArticleToTagService
    {
        Task<List<ArticleToTag>> GetArticleTags(int[]? ids, int? articleId);
        Task<List<ArticleToTag>> GetArticleTagsDelete(int[]? ids, int? articleId);

        Task<ArticleToTag> GetByIdArticleTags(int? id, int? articleId);

        Task DeleteArticleTag(List<ArticleToTag> articleToTags);
    }
}
