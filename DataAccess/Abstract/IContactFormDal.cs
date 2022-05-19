using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IContactFormDal:IEntityRepository<ContactForm>
    {
        Task<List<ContactForm>> GetAllInclude(Expression<Func<ContactForm, bool>>? filters);
        Task<ContactForm> GetByIdInclude(Expression<Func<ContactForm, bool>>? filters);
    }
}
