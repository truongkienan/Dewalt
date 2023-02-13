using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class BrandRepository: BaseRepository
    {
        public BrandRepository(IDbConnection connection) : base(connection)
        {
        }
        public Brand GetBrandById(byte id)
        {
            return connection.QuerySingleOrDefault<Brand>("SELECT * FROM Brand WHERE BrandId=@Id", new { Id = id });
        }
        public int Edit(Brand obj)
        {
            string sql = "UPDATE Brand SET BrandName=@BrandName, Description=@Description, ImageUrl=@ImageUrl WHERE BrandId=@BrandId";
            return connection.Execute(sql, obj);
        }
        public int Delete(byte id)
        {
            string sql = "DELETE Brand WHERE BrandId = @Id";
            return connection.Execute(sql, new { Id = id });
        }
        public int DeleteMultiple(List<byte> arr)
        {
            string sql = "DELETE Brand WHERE BrandId IN @Arr";
            return connection.Execute(sql, new { Arr = arr });
        }
        public IEnumerable<Brand> GetBrands()
        {
            return connection.Query<Brand>("SELECT * FROM Brand");
        }
        public int Add(Brand obj)
        {
            string sql = "INSERT INTO Brand (BrandName, Description, ImageUrl) VALUES (@Name, @Description, @ImageUrl)";
            return connection.Execute(sql, new { Name = obj.BrandName, Description = obj.Description, ImageUrl = obj.ImageUrl });
        }
    }
}
