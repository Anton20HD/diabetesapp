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
            var post = await dbContext.Posts
                .Include(p => p.User)
                .ToListAsync();


            if (post is null || post.Count == 0)
            {
                return NotFound();
            }

            return Ok(post);


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

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await dbContext.Posts.Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

            if (post is null)
            {
                return NotFound();
            }

            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();


            return Ok();

        }

    }






}
