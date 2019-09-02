using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Models;
using MovieTicketBooking.DataBaseOperations;
using Microsoft.AspNetCore.Http;

namespace MovieTicketBooking.Controllers
{
    public class UserController : Controller
    {
        UserDB UserDBObj = new UserDB();

        public IActionResult Login(int id, int seat)
        {
            if (id != 0)
            {
                HttpContext.Session.SetInt32("TimeID", id);
                HttpContext.Session.SetInt32("seat", seat);
            }
            ViewData["message"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                string status = UserDBObj.Login(user.Name, user.Password);
                if (status == "Success")
                {
                    HttpContext.Session.SetInt32("Logged", 1);
                    var id = HttpContext.Session.GetInt32("TimeID");
                    var seat = HttpContext.Session.GetInt32("seat");
                    UserInfo User = UserDBObj.Search(user);
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetInt32("UserID", User.UserID);
                    if (id != null && seat != null)
                    {
                        int MovieId = Convert.ToInt32(HttpContext.Session.GetInt32("MovieId"));
                        string date = Convert.ToString(HttpContext.Session.GetString("date"));
                        return RedirectToAction("MovieShowsOndate", "Movie", new { @MovieId = MovieId, @date = date });
                    }
                    return RedirectToAction("Home", "Movie");
                }
                else
                {
                    ViewBag.NotValidUser = status;
                }
                return View("../Movie/Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                UserInfo User = UserDBObj.SignUp(user);
                HttpContext.Session.SetInt32("Logged", 1);
                var id = HttpContext.Session.GetInt32("TimeID");
                var seat = HttpContext.Session.GetInt32("seat");
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetInt32("UserID", User.UserID);
                if (id != null && seat != null)
                {
                    int MovieId = Convert.ToInt32(HttpContext.Session.GetInt32("MovieId"));
                    string date = Convert.ToString(HttpContext.Session.GetString("date"));
                    return RedirectToAction("MovieShowsOndate", "Movie", new { @MovieId = MovieId, @date = date });
                }
                return RedirectToAction("../Movie/Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetInt32("Logged", 0);
            return RedirectToAction("../Movie/Home");
        }
    }
}