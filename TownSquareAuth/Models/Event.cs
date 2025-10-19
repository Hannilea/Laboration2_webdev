using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownSquareAuth.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        
        public ICollection <EventRSVP> RSVPs { get; set; }

    }
}