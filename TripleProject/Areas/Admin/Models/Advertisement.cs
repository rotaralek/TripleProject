using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class Advertisement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Range(0, 999999999.99)]
        public decimal? Price { get; set; }

        public Currency? Currency { get; set; }

        [Range(0, 999999999)]
        public int? Quantity { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Attribute")]
        public int? AttributeId { get; set; }
        public AdvertisementAttribute Attribute { get; set; }

        public int? ImageId { get; set; }
        public FileUpload Image { get; set; }
        public string GalleryId { get; set; }
        public FileUpload Gallery { get; set; }

        public int? Views { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateTime { get; set; }
    }
}
