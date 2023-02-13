using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class DistrictRepository : BaseRepository
    {
        public DistrictRepository(IDbConnection connection) : base(connection)
        {
        }
        public int Add(List<District> list)
        {
            string sql = "INSERT INTO District (DistrictId, ProvinceId, DistrictName, DistrictType) VALUES (@DistrictId, @ProvinceId, @DistrictName, @DistrictType)";
            return connection.Execute(sql, list);
        }

        public IEnumerable<District> GetDistricts()
        {
            return connection.Query<District>("SELECT * FROM District");
        }
        public IEnumerable<District> GetDistricts(byte id)
        {
            return connection.Query<District>("SELECT * FROM District WHERE ProvinceId=@Id", new { Id = id });
        }
    }
}
