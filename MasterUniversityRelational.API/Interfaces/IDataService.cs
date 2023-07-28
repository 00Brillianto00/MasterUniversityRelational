using System.Data;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using NUnit.Framework;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IDataService
    {

        Task<object>SaveOne(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<int> RunQuery(string query, object parameters = null, bool usingTransaction = false, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<IEnumerable<H>> GetMany<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null);

        Task<H> Get<H>(string query, object parameters = null, CommandType commandType = CommandType.Text, int? timeout = null);
    }
}
