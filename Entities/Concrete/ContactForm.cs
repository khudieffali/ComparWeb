using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class ContactForm : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string Note { get; set; } = null!;
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? Date { get; set; }
        public bool DemoRequest { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
