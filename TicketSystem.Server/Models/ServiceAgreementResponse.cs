namespace TicketSystem.Server.Models
{
    public class ServiceAgreementResponse
    {
        public int Id { get; set; }
        public required Department Department { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string CreatedBy { get; set; }
        public required string SignedBy { get; set; }
    }
}
