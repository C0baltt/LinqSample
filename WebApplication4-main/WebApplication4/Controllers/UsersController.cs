using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UsersController : Controller
    {
        private static List<User> _users = new()
        {
            new User
            {
                Id = 1, Name = "John"
            },
            new User
            {
                Id = 2, Name = "Bob"
            },
            new User
            {
                Id = 3, Name = "Albert"
            },
            new User
            {
                Id = 4, Name = "Sara"
            },
            new User
            {
                Id = 5, Name = "Mike"
            },
            new User
            {
                Id = 6, Name = "Nick"
            },
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(_users.ToArray());
        }

        [HttpGet]
        public IActionResult Details([Range(1, int.MaxValue)]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }

            return View("UserDetails", user);
        }

        [HttpGet]
        public IActionResult Edit([Range(0, int.MaxValue)] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id == 0)
            {
                return View(new User());
            }

            var user = _users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (user.IsNew)
            {
                user.Id = _users.Max(x => x.Id) + 1;
                _users.Add(user);
            }
            else
            {
                var userToUpdate = _users.Single(x => x.Id == user.Id);
                userToUpdate.Age = user.Age;
                userToUpdate.Name = user.Name;
            }

            return RedirectToAction("Index");
        }
    }
}
