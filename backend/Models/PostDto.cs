using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required DateTime PublishedDate { get; set; }

        //behålla userid här eftersom det skickas ut till klienten
        public int UserId { get; set; }

         public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}