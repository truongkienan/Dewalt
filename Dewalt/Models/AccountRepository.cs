using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class AccountRepository:BaseRepository
    {
        public AccountRepository(IDbConnection connection) : base(connection)
        {
        }
        

        public Account Login(LoginModel obj)
        {
            return connection.QuerySingleOrDefault<Account>("LoginAccount", new
            {
                Username = obj.Username,
                Password = Helper.Hash(obj.Username + "@!?^&#" + obj.Password)
            }, commandType: CommandType.StoredProcedure);
        }
      


    }
}
