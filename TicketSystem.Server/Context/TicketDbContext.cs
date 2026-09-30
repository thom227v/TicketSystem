using Npgsql;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Context
{
    public class TicketDbContext
    {
        private readonly IConfiguration _config;
        public TicketDbContext(IConfiguration config)
        {
            _config = config;
        }

        private NpgsqlConnection Connection => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();


            using (NpgsqlConnection context = Connection)
            {
                context.Open();
                using (NpgsqlCommand cmd = context.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM departments";
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        departments.Add(department);
                    }
                }
            }

            return departments;
        }

    }
}
