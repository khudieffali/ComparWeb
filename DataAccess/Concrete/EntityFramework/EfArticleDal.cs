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
                .Include(c=>c.ArticleToTags)
                .ThenInclude(c => c.Tag)
                .OrderByDescending(c=>c.CreatedDate)
                .AsQueryable();
            if (filters != null)
            {
                articles = articles.Where(filters);
            }
            return await articles.ToListAsync();
        }
        public async Task<List<Article>> GetAllHomeInclude(Expression<Func<Article, bool>>? filters)
        {
            using ComparDbContext context = new();
            var articles = context.Articles
                .Where(c => !c.IsDeleted && c.InActive)
                .Include(c => c.Category)
                .Include(c => c.ArticleImages)
                .Include(c => c.ArticleToTags)
                .ThenInclude(c => c.Tag)
                .OrderByDescending(c=>c.CreatedDate)
                .Take(3)
                .AsQueryable();

            if (filters != null)
            {
                articles = articles.Where(filters);
            }
            return await articles.ToListAsync();
        }
        public async Task<List<Article>> GetAllPopularsInclude(Expression<Func<Article, bool>>? filters)
        {
            using ComparDbContext context = new();
            var articles = context.Articles
                .Where(c => !c.IsDeleted && c.InActive)
                .Include(c => c.Category)
                .Include(c => c.ArticleImages)
                .Include(c => c.ArticleToTags)
                .ThenInclude(c => c.Tag)
                .OrderByDescending(c => c.ViewCount)
                .Take(3)
                .AsQueryable();

            if (filters != null)
            {
                articles = articles.Where(filters);
            }
            return await articles.ToListAsync();
        }
        public async Task<List<Article>> GetAllSearchInclude(Expression<Func<Article, bool>>? filters, string? q, int? categoryId)
        {
            using ComparDbContext context = new();
            var articles = context.Articles
                .Include(c => c.Category)
                .Include(c => c.ArticleImages)
                .Include(c => c.ArticleToTags)
                .ThenInclude(c => c.Tag)
                .OrderByDescending(c => c.CreatedDate)
                .AsQueryable();
            if (categoryId != null)
            {
                articles = articles.Where(x => x.CategoryId == categoryId);
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                articles = articles.Where(x => x.Name.Contains(q) || x.Category.Name.ToLower().Contains(q.ToLower()));
            }
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
                .Include(c => c.ArticleToTags)
                .ThenInclude(c=>c.Tag)
                .FirstOrDefaultAsync(filters);
            return await article;
        }

        public async Task UpdateArticle(Article article)
        {
            ComparDbContext context = new();
             context.Update(article);
            await context.SaveChangesAsync();
        }
    }
}
