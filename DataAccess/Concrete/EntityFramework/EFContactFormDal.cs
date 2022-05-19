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
    public class EFContactFormDal: EFEntityRepositoryBase<ContactForm, ComparDbContext>, IContactFormDal
    {
        public async Task<List<ContactForm>> GetAllInclude(Expression<Func<ContactForm, bool>>? filters)
        {
            using ComparDbContext context = new();
            var contact = context.ContactForms
                .Where(c => !c.IsDeleted)
                .Include(c => c.Category)
                .AsQueryable();

            if (filters != null)
            {
                contact = contact.Where(filters);
            }
            return await contact.ToListAsync();
        }

        public async Task<ContactForm> GetByIdInclude(Expression<Func<ContactForm, bool>>? filters)
        {
            using ComparDbContext context = new();
            var contact = context.ContactForms
                .Where(c => !c.IsDeleted)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(filters);
            return await contact;
        }
    }
}
