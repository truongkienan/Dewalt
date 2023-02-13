namespace Dewalt.Models
{
    public class SubCategory
    {
        public short SubCategoryId { get; set; }
        public short CategoryId { get; set; }
        public byte ColumnIndex { get; set; }
        public string SubCategoryName { get; set; }
    }
}
