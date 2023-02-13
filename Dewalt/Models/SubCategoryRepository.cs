using Dapper;
using System.Data;
namespace Dewalt.Models
{
    public class SubCategoryRepository : BaseRepository
    {
        public SubCategoryRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<SubCategory> GetSubCategories()
        {
            return connection.Query<SubCategory>("SELECT * FROM SubCategory");
        }
        public IEnumerable<SubCategory> GetSubCategoriesById(int id)
        {
            return connection.Query<SubCategory>("SELECT * FROM SubCategory WHERE CategoryId=@Id", new { Id = id });
        }
    }
}
