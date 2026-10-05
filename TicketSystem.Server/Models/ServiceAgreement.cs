using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? createdby { get; set; }
        public string? signedby { get; set; }
        [NotMapped]
        public Department? Department { get; set; }
    }
}
