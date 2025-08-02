using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CommentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // Kallar på och hämtar kommentarerna som användaren har skapat
        [HttpGet("{userId}")]
        public async Task<IActionResult> FetchUserComments(int userId)
        {
            var comments = await dbContext.Comments
                //Hämtar endast kommentarer som är skriva av den användaren
                .Where(c => c.UserId == userId)
                //Hämtar direkt den relaterade posten som kommentaren tillhör
                .Include(c => c.Post)
                // hämtar direkt användaren som skrivit kommentaren
                .Include(c => c.User)
                .ToListAsync();

            if (comments.Count == 0)
            {
                return NotFound();
            }

            var commentDtos = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                PublishedDate = c.PublishedDate,
                UserId = c.UserId,
                PostId = c.PostId,


            }).ToList();

            return Ok(commentDtos);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {

            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized("UserId not found in token");
            }

            var userId = int.Parse(userIdClaim.Value);


            var comment = new Comment()
            {
                Author = createCommentDto.Author,
                Content = createCommentDto.Content,
                PublishedDate = createCommentDto.PublishedDate,
                UserId = userId,
                PostId = createCommentDto.PostId

            };

            // EFC vill att du ska använda savechanges för att spara informationen
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();

            return Ok(new
            {
                comment.Id,
                comment.Author,
                comment.Content,
                comment.PublishedDate,
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto updateCommentDto)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment is null)
            {
                return NotFound();
            }

            comment.Author = updateCommentDto.Author;
            comment.Content = updateCommentDto.Content;
            comment.PublishedDate = updateCommentDto.PublishedDate;

            await dbContext.SaveChangesAsync();
            return Ok(comment);

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await dbContext.Comments.Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (comment is null)
            {
                return NotFound();
            }

            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();


            return Ok();

        }

    }




}
