using MasterUniversityRelational.API.Services.Databases;
using System.Data;
using System.Data.SqlClient;

namespace MasterUniversityRelational.API.Services.Databases
{
    public class SqlDataService : DataService
    {
        public SqlDataService(IConfiguration config, ILogger<DataService> logger)
           : base(config, logger)
        {
        }

        public SqlDataService(string connectionName, IConfiguration config, ILogger<DataService> logger)
            : base(connectionName, config, logger)
        {
        }

        protected override IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionName))
            {
                return new SqlConnection(_config.GetConnectionString("SqlConnectionString"));
            }

            return new SqlConnection(_config.GetConnectionString(_connectionName));
        }
    }
}
