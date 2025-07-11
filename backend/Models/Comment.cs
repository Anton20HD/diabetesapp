using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Comment
    
    {
        public int Id { get; set; }


        //User
        public required string Author { get; set; }
        public required string Content { get; set; }
        public required DateTime PublishedDate { get; set; }

        //Foreign key


        //Koppling till post
        public int PostId { get; set; }
        public Post? Post { get; set; }

        //Koppling till user 

        public int UserId { get; set; }
        public User? User { get; set; }


    }
}