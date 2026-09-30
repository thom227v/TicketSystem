using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public Guid SubmittedBy { get; set; }
        [MaxLength(255)]
        public required string Title { get; set; }
        [MaxLength(1000)]
        public required string Description { get; set; }
        public int Priority { get; set; }
        public int Category { get; set; }
    }
}
