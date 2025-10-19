using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownSquareAuth.Models
{
    public class EventRSVP
    {
        public int ID { get; set; }
        public User UserID { get; set; }
        
        public Event Event { get; set; }
        public DateTime Date { get; set; } //Date RSVP is created

        // public event ID  { get; set; }
    }
}