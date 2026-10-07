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
            Dictionary<int, string> priorityNames = _context.GetAllPriorities()
                .ToDictionary(priority => priority.id, priority => priority.prioritylabel);
            Dictionary<int, string> categoryNames = _context.GetAllCategories()
                .ToDictionary(category => category.id, category => category.categorylabel);
            Dictionary<int, string> stageNames = _context.GetAllStages()
                .ToDictionary(stage => stage.id, stage => stage.stagelabel);

            List<TicketTableDTO> ticketTableDTOs = tickets.Select(ticket => new TicketTableDTO
            {
                id = ticket.id,
                submittedby = ticket.submittedby,
                title = ticket.title,
                description = ticket.description,
                priorityname = priorityNames.GetValueOrDefault(ticket.priorityid, "Unknown"),
                categoryname = categoryNames.GetValueOrDefault(ticket.categoryid, "Unknown"),
                stagename = stageNames.GetValueOrDefault(ticket.stageid, "Unknown"),
                affectedusers = new List<string>()
            }).ToList();

            foreach (TicketTableDTO ticketTableDTO in ticketTableDTOs)
            {
                ticketTableDTO.affectedusers = _context
                    .GetAffectedPartiesNameByTicketId(ticketTableDTO.id)
                    .ToList();
            }

            return ticketTableDTOs;
        }

        public void UpdateTicket(TicketRequest request)
        {
            _context.UpdateTicket(request);
        }

        public List<Stage> GetStages()
        {
            return _context.GetAllStages();
        }

        public List<Priority> GetPriorities()
        {
            return _context.GetAllPriorities();
        }

        public List<Category> GetCategories()
        {
            return _context.GetAllCategories();
        }
    }
}
