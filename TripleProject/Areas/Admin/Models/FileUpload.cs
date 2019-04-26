using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.Models
{
    public class FileUpload 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        //[Display(Name = "Image")]
        //public byte[] ItemUpload { get; set; }
    }
}
