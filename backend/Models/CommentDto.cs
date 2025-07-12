using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public required string Author { get; set; }
        public required string Content { get; set; }
        public required DateTime PublishedDate { get; set; }


        public int PostId { get; set; }

        public int UserId { get; set; }


    }
}