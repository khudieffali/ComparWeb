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
    public class EFArticleImageDal : EFEntityRepositoryBase<ArticleImage, ComparDbContext>, IArticleImageDal
    {
        public async Task DeleteRange(List<ArticleImage> articleImages)
        {
            using ComparDbContext context = new();
            context.ArticleImages.RemoveRange(articleImages);
            await context.SaveChangesAsync();
        }

        public async Task<List<ArticleImage>> GetByIdArticleImages(Expression<Func<ArticleImage, bool>>? filters)
        {
            using ComparDbContext context = new();
            var articleImages = context.ArticleImages
                 .AsQueryable();

            if (filters != null)
            {
                articleImages = articleImages.Where(filters);
            }
            return await articleImages.ToListAsync();
        }

    }
}
