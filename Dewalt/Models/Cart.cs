namespace Dewalt.Models
{
    public class Cart
    {
        public string CartCode { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public short Quantity { get; set; }
        public decimal? Price { get; set; }        
        public string ImageUrl { get; set; }
    }
}
