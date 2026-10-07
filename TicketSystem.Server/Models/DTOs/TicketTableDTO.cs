namespace TicketSystem.Server.Models.DTOs
{
    public class TicketTableDTO
    {
        public int id { get; set; }
        public string submittedby { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string priorityname { get; set; }
        public string categoryname { get; set; }
        public string stagename { get; set; }
        public List<string> affectedusers { get; set; }
    }
}
