using Dapper;
using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models.Commons;
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

        protected DataService(IConfiguration config, ILogger<DataService> logger)
        {
            _config = config;
        }

        protected DataService(string connectionName, IConfiguration config, ILogger<DataService> logger)
        {
            _connectionName = connectionName;
            _config = config;
        }

        protected abstract IDbConnection GetConnection();

        public void BeginTransaction()
        {
            _connection = GetConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _connection.Close();
            _connection.Dispose();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _connection.Close();
            _connection.Dispose();
        }

        public async Task<object> GetScalar(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
                if (usingTransaction)
                {
                    return _connection.ExecuteScalarAsync(query, parameters, _transaction, timeout, commandType);
                }

                using IDbConnection connection = GetConnection();
                return await connection.ExecuteScalarAsync(query, parameters, null, null, commandType);
            }
            catch (Exception ex)
            { 
                throw;
            }
        }

        public async Task<int> ExecuteNonQuery<H>(string query, List<H> parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            _ = 1;
            try
            {
               // _logger.LogInformation(query);
                if (usingTransaction)
                {
                    return await _connection.ExecuteAsync(query, parameters.ToArray(), _transaction, timeout, commandType);
                }

                using IDbConnection connection = GetConnection();
                connection.Open();
                int result = await connection.ExecuteAsync(query, parameters.ToArray(), null, null, commandType);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<int> ExecuteNonQuery(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            _ = 1;
            try
            {
                //_logger.LogInformation(query);
                if (usingTransaction)
                {
                    return await _connection.ExecuteAsync(query, parameters, _transaction, timeout, commandType);
                }

                using IDbConnection connection = GetConnection();
                connection.Open();
                int result = await connection.ExecuteAsync(query, parameters, null, timeout, commandType);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<H>> GetMany<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
               // _logger.LogInformation(query);
                using IDbConnection connection = GetConnection();
                return await connection.QueryAsync<H>(query, parameters, null, timeout, commandType);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<H> GetOne<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null)
        {
            try
            {
               // _logger.LogInformation(query);
                using IDbConnection connection = GetConnection();
                return await connection.QueryFirstOrDefaultAsync<H>(query, parameters, null, timeout, commandType);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
