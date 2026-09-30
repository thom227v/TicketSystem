using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class TicketAssign
    {
        public int Id { get; set; }
        public Guid Worker { get; set; }
        public int IssueId { get; set; }
        public DateTime CreationDatetime { get; set; }
        public Guid AssignedBy { get; set; }
    }
}
