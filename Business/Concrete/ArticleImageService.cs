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
    public class ArticleImageService : IArticleImageService
    {
        private readonly IArticleImageDal _dal;

        public ArticleImageService(IArticleImageDal dal)
        {
            _dal = dal;
        }

        public async Task AddArticleImage(ArticleImage articleImage)
        {
            _dal.Add(articleImage);
        }

        public async Task DeleteArticleImages(List<ArticleImage> articleImages)
        {
           await _dal.DeleteRange(articleImages);
        }

        public async Task<List<ArticleImage>> GetByIdArticleImages(List<int>? ids, int? articleId)
        {
            var articleImages = await _dal.GetByIdArticleImages(x => ids.Contains(x.Id) && x.ArticleId == articleId);
            return articleImages;
        }
    }
}
