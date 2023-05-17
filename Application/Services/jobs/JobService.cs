using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.jobs
{
    public class JobService:IJobService 
    {
        private readonly IConfiguration _configuration;

        public JobService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void ExpireCode()
        {
            string command = $@"
               UPDATE [SendSmsCodes] SET IsExpired = CAST(1 as bit)
                where IsExpired = CAST(0 as bit) AND ExpireDate < GETDATE()
        ";

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query(command);
            }
        }
    }
}
