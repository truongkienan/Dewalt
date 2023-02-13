using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class AddressRepository : BaseRepository
    {
        public AddressRepository(IDbConnection connection) : base(connection)
        {
        }
        public int Add(Address obj)
        {
            string sql = "INSERT INTO Address(AddressName, WardId, MemberId, Phone, IsDefault) VALUES (@AddressName, @WardId, @MemberId, @Phone, @IsDefault)";
            return connection.Execute(sql, new
            {
                AddressName = obj.AddressName,
                WardId = obj.WardId,
                MemberId = obj.MemberId,
                Phone = obj.Phone,
                IsDefault = obj.IsDefault
            });
        }
        public IEnumerable<Address> GetAddresses(Guid id)
        {
            return connection.Query<Address>("GetAddresses", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
