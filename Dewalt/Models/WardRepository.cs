using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class WardRepository : BaseRepository
    {
        public WardRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Ward> GetWards()
        {
            return connection.Query<Ward>("SELECT * FROM Ward");
        }
        public IEnumerable<Ward> GetWards(short id)
        {
            return connection.Query<Ward>("SELECT * FROM Ward WHERE DistrictId=@Id", new { Id = id });
        }
        public int Add(List<Ward> list)
        {
            return connection.Execute("INSERT INTO Ward (WardId, DistrictId, WardName, WardType)" +
                "VALUES (@WardId, @DistrictId, @WardName, @WardType)", list);
        }
    }
}
