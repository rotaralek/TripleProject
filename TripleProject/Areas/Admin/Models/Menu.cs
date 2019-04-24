using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public int Order { get; set; }
    }
}
