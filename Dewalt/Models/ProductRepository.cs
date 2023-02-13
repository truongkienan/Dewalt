using Dapper;
using System.Collections;
using System.Data;

namespace Dewalt.Models
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(IDbConnection connection) : base(connection)
        {
        }
        public int Delete(int id)
        {
            return connection.Execute("UPDATE Product SET IsDeleted=1 WHERE ProductId=@Id", new { Id = id });
        }
        public int Edit(Product obj)
        {
            return connection.Execute("EditProduct", new
            {
                Id = obj.ProductId,
                BrandId = obj.BrandId,
                CategoryId = obj.CategoryId,
                SubCategoryId = obj.SubCategoryId,
                StatusId = obj.StatusId,
                ProductName = obj.ProductName,
                ProductSKU = obj.ProductSKU,
                Description = obj.Description,
                Price = obj.Price,
                ImageUrl = obj.ImageUrl,
            }, commandType: CommandType.StoredProcedure);
        }        
        public int Add(Product obj)
        {
            return connection.Execute("AddProduct", new
            {
                BrandId = obj.BrandId,
                CategoryId = obj.CategoryId,
                SubCategoryId = obj.SubCategoryId,
                StatusId=obj.StatusId,
                ProductName = obj.ProductName,
                ProductSKU=obj.ProductSKU,
                Description = obj.Description,
                Price = obj.Price,
                ImageUrl = obj.ImageUrl,
            }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<ProductStatus> GetStatus()
        {
            return connection.Query<ProductStatus>("SELECT * FROM ProductStatus");
        }
        public IEnumerable<Product> SearchProducts(string q, int page, int size, out int totalPage, out int total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Q", '%' + q + '%');
            parameters.Add("@Index", (page - 1) * size, dbType: DbType.Int32);
            parameters.Add("@Size", size, dbType: DbType.Int32);
            parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            IEnumerable<Product> list = connection.Query<Product>("SearchProducts", parameters, commandType: CommandType.StoredProcedure);
            total = parameters.Get<int>("@Total");
            totalPage = (total - 1) / size + 1;
            return list;
        }
        public IEnumerable<decimal> GetPricesByCategory(short id)
        {            
            IEnumerable<decimal> list= connection.Query<decimal>("GetPricesByCategory", new { Id = id }, commandType:CommandType.StoredProcedure);
            return list;
        }
        public IEnumerable<decimal> GetPricesBySubCategory(short id)
        {
            string sql = "SELECT Price FROM Product WHERE SubCategoryId=@Id AND (Price=(SELECT MIN(Price) FROM Product WHERE SubCategoryId=@Id ) OR Price = (SELECT MAX(Price) FROM Product WHERE SubCategoryId=@Id )) GROUP BY Price ORDER BY Price ASC";
            return connection.Query<decimal>(sql, new { Id = id });
        }
        public IEnumerable<Product> GetProductsByCategory(string col, short id, int page, int size, int order, decimal minPrice, decimal maxPrice, out int totalPage, out int total)        
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Col", col);
            parameters.Add("Id", id);
            parameters.Add("Index", (page - 1) * size, dbType: DbType.Int32);
            parameters.Add("Size", size, dbType: DbType.Int32);
            parameters.Add("Order", order, dbType: DbType.Int32);
            parameters.Add("MinPrice", minPrice, dbType: DbType.Decimal);
            parameters.Add("MaxPrice", maxPrice, dbType: DbType.Decimal);
            parameters.Add("Total", direction: ParameterDirection.Output, dbType: DbType.Int32);
            IEnumerable<Product> list = connection.Query<Product>("GetProductsByCategory", parameters, commandType: CommandType.StoredProcedure);
            total = parameters.Get<int>("@Total");
            totalPage = (total - 1) / size + 1;
            return list;            
        }
        public IEnumerable<Product> GetProductsBySubCategory(short id)
        {
            return connection.Query<Product>("SELECT * FROM Product WHERE SubCategoryId=@Id", new { Id = id });
        }
        public IEnumerable<Product> GetProducts()
        {
            return connection.Query<Product>("SELECT * FROM Product ORDER BY ProductId DESC");
        }
        public IEnumerable<Product> GetBestSellers()
        {
            return connection.Query<Product>("SELECT TOP(7) * FROM Product WHERE IsBestSeller=1 ORDER BY ProductId DESC");
        }
        public IEnumerable<Product> GetNewArrivals()
        {
            return connection.Query<Product>("SELECT * FROM Product WHERE IsNewArrival=1 ORDER BY ProductId DESC");
        }
        public IEnumerable<Product> GetRelatedProducts(int id, short categoryId)
        {
            string sql = "SELECT * FROM Product WHERE ProductId <> @Id AND CategoryId=@categoryId";
            return connection.Query<Product>(sql, new { Id = id, categoryId = categoryId });
        }
        public Product GetProductById(int id)
        {
            return connection.QuerySingleOrDefault<Product>("SELECT * FROM Product WHERE ProductId=@Id", new { Id = id });
        }
        public IEnumerable<ProductAndFeature> GetProductAndFeatures()
        {
            string sql = "SELECT Product.*, FeatureId FROM Product JOIN ProductFeature ON Product.ProductId=ProductFeature.ProductId";
            return connection.Query<ProductAndFeature>(sql);
        }
        public int RemoveVietnameseTone(List<object> list)
        {
            string sql = "UPDATE Product SET SearchAnsi=@SearchAnsi WHERE ProductId=@ProductId";
            return connection.Execute(sql, list);
        }
    }
}
