﻿using Microsoft.AspNetCore.Http;
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
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Catalog")]
        public int? CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        [Range(0, 9999.99)]
        public decimal? Price { get; set; }

        [Range(0, 1000)]
        public int? Quantity { get; set; }

        [Display(Name = "Attribute")]
        public int? AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; }
    }
}
