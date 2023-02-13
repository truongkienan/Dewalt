using System.ComponentModel.DataAnnotations;

namespace Dewalt.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3)]
        public string AddressName { get; set; }
        public Guid MemberId { get; set; }
        public int WardId { get; set; }
        public string? WardName { get; set; }
        public short DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public byte ProvinceId { get; set; }
        public string? ProvinceName { get; set; }
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(9)]
        [MaxLength(16)]
        public string Phone { get; set; }
        public bool IsDefault { get; set; }
    }
}
