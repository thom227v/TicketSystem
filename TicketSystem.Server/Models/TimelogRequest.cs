namespace TicketSystem.Server.Models
{
    public class TimelogRequest
    {
        public int TicketId { get; set; }
        public decimal TimeSpent { get; set; }
        public string Description { get; set; }
        public string? AssigneName { get; set; }
    }
}
