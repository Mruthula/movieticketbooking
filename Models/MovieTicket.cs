using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class MovieTicket
    {
        public int TimeID { get; set; }
        public string Movie { get; set; }

        public int NumberSeats { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }

    }
}
