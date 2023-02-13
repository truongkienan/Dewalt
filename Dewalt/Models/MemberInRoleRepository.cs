using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class MemberInRoleRepository : BaseRepository
    {
        public MemberInRoleRepository(IDbConnection connection) : base(connection) { }
        public int Save(MemberInRole obj)
        {
            return connection.Execute("SaveMemberInRole", obj, commandType: CommandType.StoredProcedure);
        }
    }
}
