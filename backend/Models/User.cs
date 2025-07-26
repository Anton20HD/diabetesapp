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

        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        // eventuellt lägga till datetime i framtiden 

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();



    }
}