using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public string submittedBy { get; set; }
        [MaxLength(255)]
        public required string title { get; set; }
        [MaxLength(1000)]
        public required string description { get; set; }
        public int priority { get; set; }
        public int category { get; set; }
    }
}
