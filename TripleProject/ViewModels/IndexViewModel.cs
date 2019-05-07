using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;

namespace TripleProject.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
