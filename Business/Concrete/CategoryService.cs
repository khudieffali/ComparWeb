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
    public class CategoryService : ICategoryService
    {
        ICategoryDal _dal;

        public CategoryService(ICategoryDal dal)
        {
            _dal = dal;
        }

        public async Task Add(Category category)
        {
           _dal.Add(category);
        }

        public async Task<List<Category>> GetCategories()
        {
            return _dal.GetAll(x=>!x.IsDeleted);
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            return _dal.Get(x=>x.Id==id && !x.IsDeleted);
        }

        public async Task Update(Category category)
        {
           _dal.Update(category);
        }
    }
}
