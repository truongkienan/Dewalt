namespace Dewalt.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public byte BrandId { get; set; }
        public short CategoryId { get; set; }
        public short SubCategoryId { get; set; }
        public short StatusId { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }        
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string GalleryUrl { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string SearchAnsi { get; set; }
    }
}
