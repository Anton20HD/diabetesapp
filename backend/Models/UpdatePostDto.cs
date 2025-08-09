using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class UpdatePostDto
    {   

             [Required(ErrorMessage = "Title is required")]
         public required string Title { get; set; }

              [Required(ErrorMessage = "Description is required")]
        public required string Content { get; set; }

             [Required(ErrorMessage = "Date is required")]
        public required DateTime PublishedDate { get; set; }
    }
}