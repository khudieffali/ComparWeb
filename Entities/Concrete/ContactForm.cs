using Core.Entity;

namespace Entities.Concrete
{
    public class ContactForm : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string? Note { get; set; }
        public int MyProperty { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Date { get; set; }
        public bool DemoRequest { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
    }
}
