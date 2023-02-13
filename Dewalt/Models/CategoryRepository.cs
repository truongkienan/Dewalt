using System.Data;
using Dapper;
namespace Dewalt.Models
{
    public class CategoryRepository : BaseRepository
    {
        public CategoryRepository(IDbConnection connection) : base(connection)
        {
        }
        public Category GetCategoryById(short id)
        {
            
            return connection.QuerySingleOrDefault<Category>("SELECT * FROM Category WHERE CategoryId=@Id", new { Id = id });
        }
        public IEnumerable<Category> GetCategories()
        {
            return connection.Query<Category>("SELECT * FROM Category");
        }
    }
}
