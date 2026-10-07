namespace TicketSystem.Server.Models.DTOs
{
    public class TicketTableDTO
    {
        public int id { get; set; }
        public string submittedby { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int priorityid { get; set; }
        public int categoryid { get; set; }
        public int stageid { get; set; }
        public List<string> affectedusers { get; set; }
    }
}
