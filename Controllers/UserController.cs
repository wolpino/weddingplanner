using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weddingplanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace weddingplanner.Controllers
{
    public class UserController : Controller
    {
        private WeddingContext _context;
        public UserController(WeddingContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            PasswordHasher<RegisterViewModel> hasher = new PasswordHasher<RegisterViewModel>();
            if(_context.users.Where(user => user.email == model.Email).SingleOrDefault() != null)
            ModelState.AddModelError("Email", "Username already in use, please log in or choose another username");
            if(ModelState.IsValid)
            {
                User NewUser = new User
                {
                    first_name = model.FirstName,
                    last_name = model.LastName,
                    email = model.Email,
                    password = hasher.HashPassword(model, model.Password),
                };
                User fresh = _context.users.Add(NewUser).Entity;
                _context.SaveChanges();

                HttpContext.Session.SetInt32("id", fresh.userid);
                return RedirectToAction("AllWeddings", "Wedding");
            }
            return View("Entry");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel model)
        {
            PasswordHasher<LoginViewModel> hasher = new PasswordHasher<LoginViewModel>();
            User LoggingIn = _context.users.Where(user => user.email == model.Username).SingleOrDefault();
            if(LoggingIn == null)
                ModelState.AddModelError("Username", "Invalid email login. Have you registered?");
            else if(hasher.VerifyHashedPassword(model, LoggingIn.password, model.PWD) == 0)
            {
                ModelState.AddModelError("PWD", "Close but no cigar!");
            }
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("id", LoggingIn.userid);
                return RedirectToAction("AllWeddings", "Wedding");
            }
            return View("Entry");
        }

        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            List<User> AllUsers = _context.users.ToList();
            ViewBag.allusers = AllUsers;

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
