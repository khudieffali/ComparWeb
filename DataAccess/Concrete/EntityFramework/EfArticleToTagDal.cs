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
    public class EfArticleToTagDal : EFEntityRepositoryBase<ArticleToTag, ComparDbContext>, IArticleToTagDal
    {
        public async Task DeleteRange(List<ArticleToTag> articleTags)
        {
            using ComparDbContext context = new();
            context.ArticleToTags.RemoveRange(articleTags);
            await context.SaveChangesAsync();
        }

        public async Task<List<ArticleToTag>> GetArticleToTags(Expression<Func<ArticleToTag, bool>>? filters)
        {
            using ComparDbContext context = new();
            var articleTags = context.ArticleToTags
                .Include(c => c.Tag)
                 .AsQueryable();

            if (filters != null)
            {
                articleTags = articleTags.Where(filters);
            }
            return await articleTags.ToListAsync();
        }

        public async Task<ArticleToTag> GetByIdArticleToTags(Expression<Func<ArticleToTag, bool>>? filters)
        {
          
            using ComparDbContext context = new();
            var articleTags = context.ArticleToTags
                .Include(c => c.Tag)
                .FirstOrDefaultAsync(filters);
            return await articleTags;
        }
    }
}
