using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Catalog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public virtual Catalog Parent { get; set; }
        public virtual ICollection<Catalog> Children { get; set; }

        public List<Product> Products { get; set; }
    }
}
