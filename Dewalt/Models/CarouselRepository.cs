using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class CarouselRepository : BaseRepository
    {
        public CarouselRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Carousel> GetCarousels()
        {
            string sql = "SELECT * FROM Carousel";
            return connection.Query<Carousel>(sql);
        }
    }
}
