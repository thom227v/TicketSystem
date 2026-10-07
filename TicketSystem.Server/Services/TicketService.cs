using TicketSystem.Server.Context;
using TicketSystem.Server.Models;
using TicketSystem.Server.Models.DTOs;

namespace TicketSystem.Server.Services
{
    public class TicketService
    {
        public TicketService(TicketDbContext context)
        {
            _context = context;
        }

        public TicketDbContext _context { get; }

        public void CreateTicket(TicketRequest request)
        {
            _context.CreateTicket(request);
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> tickets = _context.GetTickets();

            return tickets;
        }
        
        public List<TicketTableDTO> GetTicketsForTable()
        {
            List<Ticket> tickets = _context.GetTickets();
            List<TicketTableDTO> ticketTableDTOs = tickets.Select(ticket => new TicketTableDTO
            {
                id = ticket.id,
                submittedby = ticket.submittedby,
                title = ticket.title,
                description = ticket.description,
                priorityid = ticket.priorityid,
                categoryid = ticket.categoryid,
                stageid = ticket.stageid
            }).ToList();

            foreach (TicketTableDTO ticketTableDTO in ticketTableDTOs)
            {
                List<string> affectedusers = _context.GetAffectedPartiesNameByTicketId(ticketTableDTO.id).ToList();
                ticketTableDTO.affectedusers = affectedusers;
            }

            return ticketTableDTOs;
        }

        public void UpdateTicket(TicketRequest request)
        {
            _context.UpdateTicket(request);
        }
    }
}
