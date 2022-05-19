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
    public class ArticleService : IArticleService
    {
        private readonly IArticleDal _dal;

        public ArticleService(IArticleDal dal)
        {
            _dal = dal;
        }

        public async Task AddArticle(Article article)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            article.CreatedDate = custDateTime;
            article.UpdatedDate = custDateTime;
            _dal.AddMyArticle(article);
        }

        public async Task<Article> GetByIdArticle(int? id)
        {
            return await _dal.GetByIdInclude(x=>x.Id==id && !x.IsDeleted);
        }

        public async Task<List<Article>> GetArticles()
        {
            return await _dal.GetAllInclude(x=>!x.IsDeleted);
        }

        public async Task UpdateArticle(Article article)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            article.UpdatedDate = custDateTime;
          _dal.Update(article);
        }
    }
}
