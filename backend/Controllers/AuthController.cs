using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


//TODO: Hasha lösenord
//Lägg till [Authorize]-skyddade endpoints som kräver token

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        // Hämtar användaren från db
        private readonly ApplicationDbContext dbContext;

        // Hämtar hemligheter från appsettings eller user secrets

        private IConfiguration config;

        public AuthController(ApplicationDbContext dbContext, IConfiguration config)
        {

            this.dbContext = dbContext;
            this.config = config;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await AuthenticateUser(loginDto);

            if (user is null)

                return Unauthorized("Wrong email or password");


            var tokenString = GenerateJSONWebToken(user);
            return Ok(new { token = tokenString });

        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTKey:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //Bra för frontend sedan om vi vill extrahera userid direkt från tokenet
                new Claim("UserId", userInfo.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config["JWTKey:ValidIssuer"],
                audience: config["JWTKey:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private async Task<User?> AuthenticateUser(LoginDto loginDto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user is null)
                return null;

            if (user.Password != loginDto.Password)
                return null;

            return user;

        }
    }
}