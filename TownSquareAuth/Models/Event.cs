using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;



namespace TownSquareAuth.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }


        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<EventRSVP>? RSVPs { get; set; } = new List<EventRSVP>();

        public List<string>? Notifications { get; set; } = new List<string>();

    }
    
    
}