namespace TicketSystem.Server.Models.Auth.DTO
{
    public class SignUpDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
