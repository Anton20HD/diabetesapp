using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PostsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // Kallar på och hämtar inläggen som användaren har skapat
        public async Task<IActionResult> FetchUserDetails()
        {
            var Post = await dbContext.Posts
                .Include(p => p.User)
                .ToListAsync();

            return Ok(Post);


        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostDto createPostDto)
        {


            var post = new Post()
            {
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                PublishedDate = createPostDto.PublishedDate,
                UserId = createPostDto.UserId
            };

            // EFC vill att du ska använda savechanges för att spara informationen
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            return Ok(post);
        }
        
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post is null)
            {
                return NotFound();
            }

            post.Title = updatePostDto.Title;
            post.Content = updatePostDto.Content;
            post.PublishedDate = updatePostDto.PublishedDate;

            await dbContext.SaveChangesAsync();
            return Ok(post);

        }




        

    }
}