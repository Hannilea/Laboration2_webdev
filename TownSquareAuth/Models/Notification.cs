using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownSquareAuth.Models
{
    public class Notification
    {
        public int NotisID { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
    }
}