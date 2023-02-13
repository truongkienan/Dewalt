using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class FeatureRepository : BaseRepository
    {
        public FeatureRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Feature> GetFeatures()
        {
            return connection.Query<Feature>("SELECT * FROM Feature");
        }
    }
}
