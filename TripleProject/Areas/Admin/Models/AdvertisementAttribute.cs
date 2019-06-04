using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class AdvertisementAttribute
    {
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
    }
}