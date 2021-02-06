using Microsoft.AspNetCore.Mvc;
using PostsBoard.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PostsBoard.InputModels;
using PostsBoard.ViewModel;
using System;

namespace PostsBoard.Controllers
{
    public class PostController : Controller
    {
        private PostDbContext Context { get; set; }
        public PostController(PostDbContext context)
        {
            Context = context;
        }
        //For Post View
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View("Index");
        }

        [HttpGet("Post/{id}")]
        public async Task<IActionResult> DetailPost([FromRoute(Name = "id")] int id)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //found post
            var found = Context.Posts.SingleOrDefault(p => p.PostID == id);
            //increase view count
            found.Views += 1;
            await Context.SaveChangesAsync();

            return View("PostDetails", found);
        }

        public IActionResult AddPost()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //Show for post categories list
            var list = Context.PostCategories.ToList();

            return View(list);
        }

        [HttpPost] //Detail of function is in API controller
        public IActionResult AddPost(PostInputModel model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditPost(int id)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var post = Context.Posts.SingleOrDefault(p => p.PostID == id);
            var list = Context.PostCategories.ToList();

            return View(
                "EditPost",
                new EditPostViewModel
                {
                    Post = post,
                    Categories = list
                }
            );
        }

        public IActionResult DeletePost()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index");
        }

        //For Post Comment view

        public IActionResult CommentListPartial(
            int postId
        )
        {
            var comments = Context.Posts.Single(p => p.PostID == postId).Comments;

            return PartialView("_CommentList", comments);
        }
    }
}
