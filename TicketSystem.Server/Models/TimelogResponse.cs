namespace TicketSystem.Server.Models
{
    public class TimelogResponse
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
        public decimal TotalHoursSpent { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
