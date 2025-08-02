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
            var user = await AuthenticateUser(loginDto);

            if (user is null)

                return Unauthorized("Wrong email or password");


            var tokenString = GenerateJSONWebToken(user);
            return Ok(new { token = tokenString });

        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterDto registerDto)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser is not null)
            {
                return BadRequest("User with this email already exists");
            }

            var userEntity = new User()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Password = ""
            };

            var hasher = new PasswordHasher<User>();
            userEntity.Password = hasher.HashPassword(userEntity, registerDto.Password);


            // EFC vill att du ska använda savechanges för att spara informationen
            dbContext.Users.Add(userEntity);
            await dbContext.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email
            };

            return Ok(userDto);
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

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            Console.WriteLine($"=== TOKEN DEBUG ===");
            Console.WriteLine($"Token length: {tokenString.Length}");
            Console.WriteLine($"Dots count: {tokenString.Count(c => c == '.')}");
            Console.WriteLine($"First 100 chars: {tokenString.Substring(0, Math.Min(100, tokenString.Length))}");
            Console.WriteLine($"=================");

            return tokenString;
        }



        private async Task<User?> AuthenticateUser(LoginDto loginDto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

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