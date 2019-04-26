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
        public string Name { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public int? Order { get; set; }

        public int? ParentId { get; set; }
        public Menu Parent { get; set; }
        public virtual ICollection<Menu> Children { get; set; }
    }
}
