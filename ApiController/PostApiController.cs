using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostsBoard.Data;
using PostsBoard.InputModels;
using PostsBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsBoard.ApiController
{
    [Route("api/posts")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private readonly PostDbContext context;

        public PostApiController(PostDbContext context)
        {
            this.context = context;
        }
        //CRUD for Post
        [HttpGet] // Show post list
        public IEnumerable<Post> GetPosts()
        {
            return context.Posts.ToList();
        }

        [HttpGet("{id}")] //For Detail Post
        public Post GetPostByID(int id)
        {
            return context.Posts.SingleOrDefault(p => p.PostID == id);
        }

        // POST /api/posts/add
        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromBody] PostInputModel inputModel)
        {
            try
            {
                var category = context.PostCategories.SingleOrDefault(c => c.CategoryID == inputModel.CategoryID);
                if (category == null)
                {
                    return BadRequest("Invalid category Id");
                }

                context.Posts.Add(new Post
                {
                    Title = inputModel.Title,
                    Content = inputModel.Content,
                    CategoryID = inputModel.CategoryID,
                    PostedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    UserID = HttpContext.Session.GetInt32("USER_LOGIN_KEY").Value
                });

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")] //Edit Post
        public async Task<IActionResult> UpdatePostByID([FromBody] PostInputModel inputModel, int id) 
        {
            var foundPost = context.Posts.SingleOrDefault(p => p.PostID == id);
            if (foundPost == null)
            {
                return BadRequest("Invalid category Id");
            }

            foundPost.Title = inputModel.Title;
            foundPost.Content = inputModel.Content;
            foundPost.CategoryID = inputModel.CategoryID;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")] //Delete Post
        public async Task<IActionResult> DeletePostByID(int id)
        {
            var foundPost = context.Posts.SingleOrDefault(p => p.PostID == id);
            if(foundPost == null)
            {
                return BadRequest();
            }

            context.Posts.Remove(foundPost);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("like")]
        public async Task<IActionResult> IncrementPostLike(int postId)
        {
            var foundPost = context.Posts.SingleOrDefault(p => p.PostID == postId);
            if (foundPost == null)
            {
                return BadRequest("Invalid post Id");
            }

            foundPost.Likes += 1;
            await context.SaveChangesAsync();

            return Ok(foundPost);
        }

        //CRUD for Post Comment

        [HttpGet("listcomment")] // Show post list
        public IEnumerable<PostComment> GetComment()
        {
            return context.Comments.ToList();
        }


        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment(
            int postId,
            [FromBody] PostCommentInputModel inputModel
        )
        {
            try
            {
                context.Comments.Add(new PostComment
                {
                    Content = inputModel.Content,
                    CommentedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    PostID = postId,
                    UserID = HttpContext.Session.GetInt32("USER_LOGIN_KEY").Value
                });

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("comment/like")]
        public async Task<IActionResult> IncrementCommentLike(int commentId)
        {
            var foundComment = context.Comments.SingleOrDefault(c => c.CommentID == commentId);
            if (foundComment == null)
            {
                return BadRequest("Invalid comment Id");
            }

            foundComment.Likes += 1;
            await context.SaveChangesAsync();

            return Ok(foundComment);
        }

        [HttpPut("comment/{id}")] //Edit Post
        public async Task<IActionResult> UpdateCommentByID([FromBody] PostCommentInputModel inputModel, int id)
        {
            var foundComment = context.Comments.SingleOrDefault(c => c.CommentID == id);
            if (foundComment == null)
            {
                return BadRequest("Invalid comment Id");
            }

           
            foundComment.Content = inputModel.Content;

            await context.SaveChangesAsync();

            return Ok(foundComment);
        }

        [HttpDelete("comment/{id}")] //Delete Comment
        public async Task<IActionResult> DeleteCommentByID(int id)
        {
            var foundComment = context.Comments.SingleOrDefault(c => c.CommentID == id);
            if (foundComment == null)
            {
                return BadRequest();
            }

            context.Comments.Remove(foundComment);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
