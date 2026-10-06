namespace TicketSystem.Server.Models
{
    public class TicketRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int Priority { get; set; } = 0;
        public int Category { get; set; } = 0;
        public string SubmittedBy { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
