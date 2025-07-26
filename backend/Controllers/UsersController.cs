using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //localhost:xxxx/api/
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        //Gör alla crud metoder asynkrona
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            //Returnerar ett 200 meddelande
            return Ok(dbContext.Users.ToList());
        }
        
        
       

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Gör alla crud metoder asynkrona
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.Password = updateUserDto.Password;

            dbContext.SaveChanges();
            return Ok(user);

        }

        
        //Gör alla crud metoder asynkrona
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();


            return Ok();

        }
        
    }
}