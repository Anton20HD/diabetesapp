using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Identity;
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
            //Modelstate används för att undvika fel som att försöka logga in med null, buggar etc
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            var user = await AuthenticateUser(loginDto);

            if (user is null)

                return Unauthorized("Ínvalid credentials");


            var tokenString = GenerateJSONWebToken(user);
            return Ok(new { token = tokenString });

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email already exists");
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email
            };


            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, registerDto.Password);

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok("User registered successfully");

        }

       
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTKey:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(ClaimTypes.Email, userInfo.Email),
        new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
            var email = loginDto.Email.Trim().ToLower();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (user is null)
                return null;

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return user;

        }
    }
}