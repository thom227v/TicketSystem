using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class Timelog
    {
        public int id { get; set; }
        public int ticketassignid { get; set; }
        public decimal totalhoursspent { get; set; }
        [MaxLength(1000)]
        public required string description { get; set; }
        public DateTime creationdate { get; set; }
    }
}
