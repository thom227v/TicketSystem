using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class ServiceAgreementCreationResponse
    {
        public int DepartmentId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
