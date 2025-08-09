using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class UpdateUserDto
    {

        public required string FirstName { get; set; }

        public required string LastName { get; set; }


        public required string Email { get; set; }


        public required string Password { get; set; }
    }
}