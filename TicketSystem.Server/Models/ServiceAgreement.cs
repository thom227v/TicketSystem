using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Server.Models
{
    public class ServiceAgreement
    {
        public int id { get; set; }
        public int departmentid { get; set; }
        [MaxLength(255)]
        public required string title { get; set; }
        [MaxLength(1000)]
        public required string description { get; set; }
        public string createdBy { get; set; }
        public string signedBy { get; set; }

        public Department? Department { get; set; }
    }
}
