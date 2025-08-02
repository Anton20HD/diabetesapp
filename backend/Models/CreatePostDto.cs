using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class CreatePostDto
    {

        public required string Title { get; set; }
        public required string Content { get; set; }

        public required DateTime PublishedDate { get; set; }

    }
}