using TicketSystem.Server.Context;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Services
{
    public class DepartmentService
    {
        private readonly TicketDbContext _context;
        public DepartmentService(TicketDbContext context)
        {
            _context = context;
        }

        public List<Department> GetDepartments ()
        {
            return _context.GetAllDepartments();
        }

        public void CreateDepartment(string departmentName)
        {
            _context.CreateDepartment(departmentName);
        }
    }
}