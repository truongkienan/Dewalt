using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class ProvinceRepository : BaseRepository
    {
        public ProvinceRepository(IDbConnection connection) : base(connection)
        {
        }
        public int Add(List<Province> list)
        {
            return connection.Execute("INSERT INTO Province (ProvinceId, ProvinceName, ProvinceType) VALUES (@ProvinceId, @ProvinceName, @ProvinceType)", list);
        }

        public IEnumerable<Province> GetProvinces()
        {
            return connection.Query<Province>("SELECT * FROM Province");
        }
    }
}
