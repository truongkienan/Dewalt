using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class CartRepository : BaseRepository
    {
        public CartRepository(IDbConnection connection) : base(connection)
        {
        }
        public int Count(string code)
        {
            string sql = "SELECT SUM(Quantity) FROM Cart WHERE CartCode=@Code";
            return connection.ExecuteScalar<int>(sql, new { Code = code });
        }
        public int Save(Cart obj)
        {
            return connection.Execute("SaveCart", new { Code = obj.CartCode, ProductId = obj.ProductId, Quantity = obj.Quantity },
                commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Cart> GetCarts(string code, out decimal total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CartCode", code);
            parameters.Add("@Total", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            IEnumerable<Cart> list= connection.Query<Cart>("GetCarts", parameters, commandType: CommandType.StoredProcedure);
            if (list.Count()>0)
            {
                total = parameters.Get<decimal>("@Total");
                return list;
            }
            total = 0;
            return null;            
        }
        public int Edit(Cart obj)
        {
            return connection.Execute("UPDATE Cart SET Quantity=@Quantity, UpdateDate=GETDATE() WHERE CartCode=@Code AND ProductId=@ProductId",
                new { Code = obj.CartCode, ProductId = obj.ProductId, Quantity = obj.Quantity });
        }
        public int Delete(Cart obj)
        {
            return connection.Execute("DELETE FROM Cart WHERE CartCode=@Code AND ProductId=@ProductId", new
            {
                Code = obj.CartCode,
                ProductId = obj.ProductId
            });
        }
    }
}
