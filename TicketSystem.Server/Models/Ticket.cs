using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public string submittedby { get; set; }
        [MaxLength(255)]
        public required string title { get; set; }
        [MaxLength(1000)]
        public required string description { get; set; }
        public int priorityid { get; set; }
        public int categoryid { get; set; }
        public int stageid { get; set; }
    }
}
