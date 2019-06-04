using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
    }
}