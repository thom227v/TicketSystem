using System.Text.Json.Serialization;

namespace TicketSystem.Server.Models
{
    public class Department
    {
        public int id { get; set; }
        public required string name { get; set; }
        [JsonIgnore]
        public ICollection<ServiceAgreement> ServiceAgreements { get; set; } = new List<ServiceAgreement>();
    }
}
