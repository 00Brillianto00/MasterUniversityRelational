using System.Data;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MasterUniversityRelational.API.Models.Commons;
using Dapper;
using NUnit.Framework;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IDataService
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        Task<object> GetScalar(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<int> ExecuteNonQuery<H>(string query, List<H> parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<int> ExecuteNonQuery(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<IEnumerable<H>> GetMany<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<H> GetOne<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null);

        //string buildwhereclause(List<DataWhereClause> whereclauses);

        //DynamicParameters getparameters(List<DataWhereClause> whereclauses);
    }
}
