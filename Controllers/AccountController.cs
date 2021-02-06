using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostsBoard.Data;
using PostsBoard.Models;
using PostsBoard.ViewModel;
using System.Linq;


namespace PostsBoard.Controllers
{
    public class AccountController : Controller
    {
        private PostDbContext Context { get; set; }

        public AccountController(PostDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var user = Context.Users.FirstOrDefault(u => u.UserName.Equals(model.UserName) && u.Password.Equals(model.Password));

                if (user != null)
                {
                    HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserID);

                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError(string.Empty, "Username or Password is not correct!");

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                Context.Users.Add(model);
                Context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
