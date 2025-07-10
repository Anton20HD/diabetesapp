using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Comments
    {
        public int Id { get; set; }


        //User
        public required string Author { get; set; }
        public required string Content { get; set; }
        public required DateTime PublishedDate { get; set; }

        //Foreign key

        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}