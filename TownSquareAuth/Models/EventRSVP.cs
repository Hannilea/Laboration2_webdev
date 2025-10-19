using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TownSquareAuth.Models;

public class EventRSVP
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public bool IsAttending { get; set; }

}
