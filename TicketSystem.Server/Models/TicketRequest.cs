namespace TicketSystem.Server.Models
{
    public class TicketRequest
    {
        public int? id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PriorityId { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public int StageId { get; set; } = 0;
        public string SubmittedBy { get; set; } = string.Empty;
        public List<string> Usernames { get; set; } = new();
    }
}
