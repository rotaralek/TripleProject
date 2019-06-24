using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Url")]
        public string Url { get; set; }
        [Display(Name = "Target")]
        public string Target { get; set; }
        [Display(Name = "Order")]
        public int? Order { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }
        [Display(Name = "Parent")]
        public Menu Parent { get; set; }
        [Display(Name = "Children")]
        public ICollection<Menu> Children { get; set; }
    }
}
