using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models.DTOs
{
    public class TicketTableDTO
    {
        public int id { get; set; }
        public string submittedby { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int priority { get; set; }
        public int category { get; set; }
        public List<string> affectedusers { get; set; }
    }
}
