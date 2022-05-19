using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IArticleService
    {
        Task<List<Article>> GetArticles();
        Task<Article> GetByIdArticle(int? id);
        Task AddArticle(Article article);
        Task UpdateArticle(Article article);
    }
}
