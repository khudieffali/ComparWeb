using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int? id);
        Task Add(Category category);
        Task Update(Category category);
    }
}
