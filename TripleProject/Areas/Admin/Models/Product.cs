using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(0, 999999999.99)]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Currency")]
        public Currency? Currency { get; set; }

        [Range(0, 999999999)]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Catalog")]
        public ICollection<ProductCatalog> ProductsCatalogs { get; set; }

        [Display(Name = "Attribute")]
        public int? AttributeId { get; set; }

        [Display(Name = "Attribute")]
        public ProductAttribute Attribute { get; set; }

        public int? ImageId { get; set; }
        public FileUpload Image { get; set; }
        public string GalleryId { get; set; }
        public FileUpload Gallery { get; set; }

        [Display(Name = "Views")]
        public int? Views { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateTime { get; set; }
    }

    public enum Currency
    {
        MDL,
        EUR,
        USD
    }
}
