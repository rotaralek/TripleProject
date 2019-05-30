using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class AdvertisementCategory
    {
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}