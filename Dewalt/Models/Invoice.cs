namespace Dewalt.Models
{
    public class Invoice
    {
        public string InvoiceId { get; set; }
        public string CartCode { get; set; }
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string WardType { get; set; }
        public string WardName { get; set; }
        public string DistrictType { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceType { get; set; }
        public string ProvinceName { get; set; }
        public Guid MemberId { get; set; }
        public string Username { get; set; }
        public DateTime InvoiceDate { get; set; }
        public byte StatusId { get; set; }
        public string StatusName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long Total { get; set; }
        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
