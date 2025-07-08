using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Post
    {
        //Int funkar men Guid kan vara bättre för bla säkerhet och exportering av data mellan olika system
        public int Id { get; set; } // unikt id

        // required förhindrar ofullständiga objekt, kompatibelt med null-säkerhet, slipper skapa konstruktor
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required DateTime PublishedDate { get; set; }

          //Foreign key
        public int UserId { get; set; }
        public  User? User { get; set; }

        //List<Comments> funkar också men ICollection är mer flexibelt
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();

    }
}