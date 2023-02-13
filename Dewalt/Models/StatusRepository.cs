using System.Data;
using Dewalt.Models;
using Dapper;
namespace Dewalt.Models
{
    public class StatusRepository : BaseRepository
    {
        public StatusRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Status> GetStatuses()
        {
            return connection.Query<Status>("SELECT * FROM Status");
        }
    }
}
