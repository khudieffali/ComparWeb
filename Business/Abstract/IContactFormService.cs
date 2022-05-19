using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IContactFormService
    {
        public Task<List<ContactForm>> GetContactForms();
        Task<ContactForm> GetContactFormById(int? id);
        Task Add(ContactForm contact);
        Task Update(ContactForm contact);
    }
}
