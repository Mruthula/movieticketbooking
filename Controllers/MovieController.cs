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
    public class MovieController : Controller
    {
        public List<MovieInfo> MovieList = new List<MovieInfo>();
        MovieDB MovieDBObj = new MovieDB();

        public IActionResult Home()
        {
            MovieList = MovieDBObj.GetMovies();
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            ViewData["message"] = logged;
            return View(MovieList);
        }
       
        public IActionResult MovieOnDate(string date )//Shows the lost of movies on a perticular date
        {
            if (ModelState.IsValid)
            {
                MovieList = MovieDBObj.MovieDate(date);
                int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
                return View(MovieList);
            }
            else
            {
                return RedirectToAction("Home", "Movie");
            }
        }

        public IActionResult MovieDetails(int id)//shows the details of a perticular movie
        {
            MovieInfo Movie = MovieDBObj.GetMovie(id);
            int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
            return View(Movie);
        }

        public IActionResult ChooseDate(int id,string name)//for selecting date after selecting movie
        {
            if (ModelState.IsValid)
            {
                MovieInfo Movie = new MovieInfo();
                Movie.MovieID = id;
                Movie.MovieName = name;
                int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
                return View(Movie);
            }
            else
            {
                return RedirectToAction("Home","Movie");
            }
        }

        public IActionResult MovieShows(int MovieId,string date)//Show times of a perticular movie on a perticular date
        {
            if (ModelState.IsValid)
            {
                if (date == null)
                {
                    MovieList = MovieDBObj.shows(MovieId);
                    int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
                    ViewData["message"] = logged;
                    return View(MovieList);

                }
                else
                {
                    HttpContext.Session.SetInt32("MovieId", MovieId);
                    HttpContext.Session.SetString("date", date);
                    int logged = Convert.ToInt32(HttpContext.Session.GetInt32("Logged"));
                    MovieList = MovieDBObj.showsMovieDate(MovieId, date);
                    ViewData["message1"] = date;
                    ViewData["message2"] = logged;
                    return View(MovieList);
                }
            }
            else
            {
                return RedirectToAction("ChooseDate", "Movie",new {id=MovieId });
            }
        }


        [HttpGet]
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
