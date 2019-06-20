using Microsoft.AspNetCore.Identity;
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
        [StringLength(300)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Range(0, 999999999)]
        [Display(Name = "Price")]
        public int? Price { get; set; }

        [Display(Name = "Currency")]
        public Currency? Currency { get; set; }

        [Range(0, 999999999)]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Category")]
        public ICollection<AdvertisementCategory> AdvertisemetsCategories { get; set; }

        [Display(Name = "Attribute")]
        public ICollection<AdvertisementAttribute> AdvertisementsAttributes { get; set; }

        [Display(Name = "Image")]
        public int? ImageId { get; set; }
        [Display(Name = "Image")]
        public FileUpload Image { get; set; }
        [Display(Name = "Gallery")]
        public string GalleryId { get; set; }
        [Display(Name = "Gallery")]
        public FileUpload Gallery { get; set; }

        [Display(Name = "Views")]
        public int? Views { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateTime { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
