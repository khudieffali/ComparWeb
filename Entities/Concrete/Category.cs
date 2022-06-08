using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [Column(TypeName = "varchar")]
        [RegularExpression("^[a-zA-z0-9_]*$", ErrorMessage = "Only Alphabets,Numbers and _ symbol allowed.")]
        public string Slug { get; set; }=null!;
        public string? PhotoUrl { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Article>? Articles { get; set; }
    }
}
