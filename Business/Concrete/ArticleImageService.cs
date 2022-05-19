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
        IArticleImageDal _dal;

        public ArticleImageService(IArticleImageDal dal)
        {
            _dal = dal;
        }

        public async Task AddArticleImage(ArticleImage articleImage)
        {
            _dal.Add(articleImage);
        }
    }
}
