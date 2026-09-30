using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class ServiceAgreement
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [MaxLength(255)]
        public required string Title { get; set; }
        [MaxLength(1000)]
        public required string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid SignedBy { get; set; }
    }
}
