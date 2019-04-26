using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Areas.Admin.ViewModels
{
    public class FileUploadViewModel
    {
        [Display(Name = "Image")]
        public IFormFile ItemUpload { get; set; }
    }
}
