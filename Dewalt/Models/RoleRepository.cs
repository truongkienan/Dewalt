using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class RoleRepository : BaseRepository
    {
        public RoleRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Role> GetRoles()
        {
            return connection.Query<Role>("SELECT * FROM Role");
        }
        public int Add(Role obj)
        {
            return connection.Execute("INSERT INTO Role (RoleName) VALUES (@Name)", new
            {
                Name = obj.RoleName
            });
        }
        public IEnumerable<RoleChecked> GetRolesCheckedByMember(Guid id)
        {
            return connection.Query<RoleChecked>("GetRolesCheckedByMember", new { MemberId = id }, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Role> GetRolesByMember(Guid id)
        {
            return connection.Query<Role>("GetRolesByMember", new { MemberId = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
