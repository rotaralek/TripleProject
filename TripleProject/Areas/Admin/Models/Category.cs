using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }
        [Display(Name = "Parent")]
        public virtual Category Parent { get; set; }
        [Display(Name = "Children")]
        public virtual ICollection<Category> Children { get; set; }

        [Display(Name = "Advertisemets")]
        public ICollection<AdvertisementCategory> AdvertisemetsCategories { get; set; }
    }
}
