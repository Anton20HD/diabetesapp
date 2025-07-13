using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User
    {
        //Int funkar men Guid kan vara bättre för bla säkerhet och exportering av data mellan olika system
        public int Id { get; set; } // unikt id

        // required förhindrar ofullständiga objekt, kompatibelt med null-säkerhet, slipper skapa konstruktor
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        // eventuellt lägga till datetime i framtiden 

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();



    }
}