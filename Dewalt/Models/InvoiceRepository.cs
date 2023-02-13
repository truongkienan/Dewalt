using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class InvoiceRepository : BaseRepository
    {
        public InvoiceRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Invoice> GetInvoices()
        {
            string sql = "SELECT Invoice.*, Member.Username, Address.AddressName, Ward.WardType, Ward.WardName, District.DistrictType, District.DistrictName,Province.ProvinceType, Province.ProvinceName, Status.StatusName, " +
                "(SELECT SUM(Quantity*Price) FROM InvoiceDetail WHERE InvoiceDetail.InvoiceId=Invoice.InvoiceId) AS Total " +
                "FROM Invoice " +
                        "JOIN Member ON Invoice.MemberId = Member.MemberId "+
                        "JOIN Address ON Invoice.AddressId = Address.AddressId "+
                        "JOIN Status ON Invoice.StatusId = Status.StatusId "+
                        "JOIN Ward ON Address.WardId = Ward.WardId "+
                        "JOIN District ON Ward.DistrictId = District.DistrictId "+
                        "JOIN Province ON District.ProvinceId = Province.ProvinceId";
            return connection.Query<Invoice>(sql);
        }
        public int UpdateStatus(Invoice obj)
        {
            string sql = "UPDATE Invoice SET StatusId=@StatusId WHERE InvoiceId =@Id";

            return connection.Execute(sql, new { Id=obj.InvoiceId, StatusId=obj.StatusId});
        }
        public Invoice GetInvoiceById(string id)
        {
            var grid = connection.QueryMultiple("GetInvoiceById", new { Id = id },
                 commandType: CommandType.StoredProcedure);

            var obj = grid.ReadSingleOrDefault<Invoice>();
            obj.InvoiceDetails = grid.Read<InvoiceDetail>();
            return obj;
        }
        public int Add(Invoice obj)
        {
            obj.InvoiceId = Helper.RandomString(8);
            return connection.Execute("AddInvoice", new
            {
                AddressId = obj.AddressId,
                CartCode = obj.CartCode,
                MemberId = obj.MemberId,
                InvoiceId = obj.InvoiceId
            }, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<StatisticInvoice> StatisticInvoices()
        {
            string sql = "SELECT MONTH(InvoiceDate) Month, COUNT(Invoice.InvoiceId) Count, SUM(Price) Sum FROM InvoiceDetail JOIN Invoice ON InvoiceDetail.InvoiceId=Invoice.InvoiceId GROUP BY MONTH(InvoiceDate)";
            return connection.Query<StatisticInvoice>(sql);
        }
    }
}
