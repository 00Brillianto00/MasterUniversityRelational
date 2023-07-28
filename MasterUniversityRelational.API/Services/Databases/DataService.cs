using Dapper;
using MasterUniversityRelational.API.Interfaces;
using System.Data;
using System.Text;

namespace MasterUniversityRelational.API.Services.Databases
{
    public abstract class DataService : IDataService
    {
        protected string _connectionName;

        protected IConfiguration _config;

        protected IDbTransaction _transaction;

        protected IDbConnection _connection;

        protected DataService(IConfiguration config)
        {
            _config = config;
        }

        protected DataService(string connectionName, IConfiguration config)
        {
            _connectionName = connectionName;
            _config = config;
        }
        protected abstract IDbConnection GetConnection();

        public async Task<object> SaveOne(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
                if (usingTransaction)
                {
                    return _connection.ExecuteScalarAsync(query, parameters, _transaction, timeout, commandType);
                }

                using IDbConnection dbConnection = GetConnection();
                return await dbConnection.ExecuteScalarAsync(query, parameters, null, null, commandType);
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }
        public async Task<int> RunQuery(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
                if (usingTransaction)
                {
                    return await _connection.ExecuteAsync(query, parameters, _transaction, timeout, commandType);
                }

                using IDbConnection dbConnection = GetConnection();
                dbConnection.Open();
                int result = await dbConnection.ExecuteAsync(query, parameters, null, timeout, commandType);
                dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<H>> GetMany<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
                using IDbConnection dbConnection = GetConnection();
                return await dbConnection.QueryAsync<H>(query, parameters, null, timeout, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<H> Get<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
                using IDbConnection dbConnection = GetConnection();
                return await dbConnection.QueryFirstOrDefaultAsync<H>(query, parameters, null, timeout, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
