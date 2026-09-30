namespace TicketSystem.Server.Models
{
    public class AffectedParty
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int IssueId { get; set; }
    }
}
