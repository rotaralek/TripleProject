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
        [StringLength(300)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        [Display(Name = "Parent")]
        public virtual Catalog Parent { get; set; }

        [Display(Name = "Children")]
        public virtual ICollection<Catalog> Children { get; set; }

        [Display(Name = "Products")]
        public ICollection<ProductCatalog> ProductsCatalogs { get; set; }
    }
}
