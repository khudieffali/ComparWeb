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
    public class ArticleToTagService:IArticleToTagService
    {
        private readonly IArticleToTagDal _dal;

        public ArticleToTagService(IArticleToTagDal dal)
        {
            _dal = dal;
        }

        public async Task DeleteArticleTag(List<ArticleToTag> articleToTags)
        {
            await _dal.DeleteRange(articleToTags);
        }

        public async Task<List<ArticleToTag>> GetArticleTags(int[]? ids, int? articleId)
        {
            return await _dal.GetArticleToTags(x => ids.Contains(x.TagId) && x.ArticleId == articleId);
        }

        public async Task<List<ArticleToTag>> GetArticleTagsDelete(int[]? ids, int? articleId)
        {
            return await _dal.GetArticleToTags(x => !ids.Contains(x.TagId) && x.ArticleId == articleId);
        }

        public Task<ArticleToTag> GetByIdArticleTags(int? id, int? articleId)
        {
            return _dal.GetByIdArticleToTags(x => x.TagId != id && x.ArticleId == articleId);
        }
    }
}
