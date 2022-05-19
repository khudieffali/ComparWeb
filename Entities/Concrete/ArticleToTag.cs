using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ArticleToTag:IEntity
    {
        public int Id { get; set; }
        public int? ArticleId { get; set; }
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
        public  Article? Article { get; set; }
    }
}
