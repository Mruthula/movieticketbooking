using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class MovieInfo
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string Pic { get; set; }
        [Display(Name = "Film Director")]

        public string Director { get; set; }
        public string Actor { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime Release { get; set; }
        public string Language { get; set; }
        public int TimeID { get; set; }
        public string Time { get; set; }
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Range(09/04/2019,09/06/2019, ErrorMessage = "Choose date between 09/04/2019 and 09/06/2019")]
        [Required(ErrorMessage = "Please select date")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please Enter number of seats")]
        public int Seat { get; set; }
        public int BookID { get; set; }

    }
}
