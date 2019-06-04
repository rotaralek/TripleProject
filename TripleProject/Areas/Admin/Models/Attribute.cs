using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Attribute
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

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        [Display(Name = "Parent")]
        public virtual Attribute Parent { get; set; }

        [Display(Name = "Children")]
        public virtual ICollection<Attribute> Children { get; set; }

        [Display(Name = "Products")]
        public ICollection<ProductAttribute> ProductsAttributes { get; set; }

        [Display(Name = "Attribute")]
        public ICollection<AdvertisementAttribute> AdvertisementsAttributes { get; set; }
    }
}
