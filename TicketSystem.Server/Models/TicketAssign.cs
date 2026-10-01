using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class TicketAssign
    {
        public int id { get; set; }
        public string worker { get; set; }
        public int ticketid { get; set; }
        public DateTime creationdatetime { get; set; }
        public string assignedby { get; set; }
    }
}
