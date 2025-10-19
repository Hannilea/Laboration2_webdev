using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownSquareAuth.Models
{
    public class ApplicationUser
    {
        public int UserID { get; set; }
        public string Name { get; set; }


        public ICollection<Event> Events { get; set; }
       public ICollection<EventRSVP> RSVPs { get; set; }

        
    }
}