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
    public class BookingController : Controller
    {
        MovieDB MovieDBObj = new MovieDB();
        public List<MovieInfo> MovieList = new List<MovieInfo>();
        public IActionResult BookSeat(int id, int seat)//page for getting the number of seats to be booked
        {
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            if (logged == 1)
            {
                if (id != 0)
                {
                    HttpContext.Session.SetInt32("TimeID", id);
                }
                var Seat = HttpContext.Session.GetInt32("seat");
                ViewData["message1"] = logged;
                ViewData["message"] = Seat;// passing number of available seats to view for limiting booking 
                return View();
            }
            else
                return RedirectToAction("../Movie/Home");
        }
        [HttpPost]
        public IActionResult BookSeat(MovieTicket NewBook)//booking the number of seats
        {
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            if (logged == 1)
            {
                var id = Convert.ToInt32(HttpContext.Session.GetInt32("TimeID"));
                var Name = HttpContext.Session.GetString("UserName");
                var UserID = HttpContext.Session.GetInt32("UserID");
                NewBook.UserID = Convert.ToInt32(UserID);
                NewBook.TimeID = id;
                NewBook.Name = Convert.ToString(Name);
                MovieDBObj.Booking(id, NewBook);
                MovieDBObj.UpdateSeat(id, NewBook.NumberSeats);
                return RedirectToAction("../Movie/Home");
            }
            else
                return RedirectToAction("../Movie/Home");
        }
        public IActionResult BookHistory()// for showing the booking history of a user
        {
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            if (logged == 1)
            {
                int UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                MovieList = MovieDBObj.BookedList(UserID);
                return View(MovieList);
            }
            else
                return RedirectToAction("../Movie/Home");
        }
        [HttpGet]
        public IActionResult CancelBooking(int bookid)//for canceling the booking
        {
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            if (logged == 1)
            {
                MovieInfo Movie = new MovieInfo();
                Movie = MovieDBObj.BookedMovie(bookid);
                return View(Movie);
            }
            else
                return RedirectToAction("../Movie/Home");
        }
        [HttpPost]
        public IActionResult CancelBooking(MovieInfo Movie)
        {
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            if (logged == 1)
            {
                MovieDBObj.Cancel(Movie.BookID, Movie.Seat);
            }
            return RedirectToAction("../Movie/Home");
        }
    }
}