using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class TicketAssign
    {
        public int Id { get; set; }
        public string Worker { get; set; }
        public int IssueId { get; set; }
        public DateTime CreationDatetime { get; set; }
        public string AssignedBy { get; set; }
    }
}
