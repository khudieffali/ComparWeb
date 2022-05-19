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
    public class ContactFormService : IContactFormService
    {
        private readonly IContactFormDal _dal;

        public ContactFormService(IContactFormDal dal)
        {
            _dal = dal;
        }

        public async Task Add(ContactForm contact)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            contact.CreatedDate = custDateTime;
            _dal.Add(contact);
        }

        public async Task<ContactForm> GetContactFormById(int? id)
        {
          return await _dal.GetByIdInclude(x=>!x.IsDeleted && x.Id == id);    
        }

        public async Task<List<ContactForm>> GetContactForms()
        {
            return await _dal.GetAllInclude(x=>!x.IsDeleted);
        }
        public async Task Update(ContactForm contact)
        {
           _dal.Update(contact);
        }
    }
}
