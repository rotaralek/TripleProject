using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class ProductCatalog
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
    }
}