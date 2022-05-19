using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfArticleDal : EFEntityRepositoryBase<Article, ComparDbContext>, IArticleDal
    {
        public async Task AddMyArticle(Article article)
        {
            ComparDbContext context = new();
            await context.AddAsync(article);
            await context.SaveChangesAsync();

        }

        public async Task<List<Article>> GetAllInclude(Expression<Func<Article, bool>>? filters)
        {
            using ComparDbContext context = new();
            var articles = context.Articles
                .Where(c => !c.IsDeleted)
                .Include(c=>c.Category)
                .Include(c => c.ArticleImages)
                .AsQueryable();

            if (filters != null)
            {
                articles = articles.Where(filters);
            }
            return await articles.ToListAsync();
        }

        public async Task<Article> GetByIdInclude(Expression<Func<Article, bool>>? filters)
        {
            using ComparDbContext context = new();
            var article = context.Articles
                .Where(c => !c.IsDeleted)
                .Include(c => c.Category)
                .Include(c => c.ArticleImages)
                .FirstOrDefaultAsync(filters);
            return await article;
        }
    }
}
