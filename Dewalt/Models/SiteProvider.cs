namespace Dewalt.Models
{
    public class SiteProvider : BaseProvider
    {
        public SiteProvider(IConfiguration configuration) : base(configuration)
        {
        }

        ContactRepository contact;
        public ContactRepository Contact
        {
            get
            {
                if (contact is null)
                {
                    contact = new ContactRepository(Connection);
                }
                return contact;
            }
        }

        MemberInRoleRepository memberInRole;
        public MemberInRoleRepository MemberInRole
        {
            get
            {
                if (memberInRole is null)
                {
                    memberInRole = new MemberInRoleRepository(Connection);
                }
                return memberInRole;
            }
        }

        StatusRepository status;
        public StatusRepository Status
        {
            get
            {
                if (status is null)
                {
                    status = new StatusRepository(Connection);
                }
                return status;
            }
        }
        RoleRepository role;
        public RoleRepository Role
        {
            get
            {
                if (role is null)
                {
                    role = new RoleRepository(Connection);
                }
                return role;
            }
        }

        BrandRepository brand;
        public BrandRepository Brand
        {
            get
            {
                if (brand is null)
                {
                    brand = new BrandRepository(Connection);
                }
                return brand;
            }
        }

        InvoiceRepository invoice;
        public InvoiceRepository Invoice
        {
            get
            {
                if (invoice is null)
                {
                    invoice = new InvoiceRepository(Connection);
                }
                return invoice;
            }
        }
        AddressRepository address;
        public AddressRepository Address
        {
            get
            {
                if (address is null)
                {
                    address = new AddressRepository(Connection);
                }
                return address;
            }
        }

        WardRepository ward;
        public WardRepository Ward
        {
            get
            {
                if (ward is null)
                {
                    ward = new WardRepository(Connection);
                }
                return ward;
            }
        }

        DistrictRepository district;
        public DistrictRepository District
        {
            get
            {
                if (district is null)
                {
                    district = new DistrictRepository(Connection);
                }
                return district;
            }
        }

        ProvinceRepository province;
        public ProvinceRepository Province
        {
            get
            {
                if (province is null)
                {                    
                    province = new ProvinceRepository(Connection);
                }
                return province;
            }
        }

        MemberRepository member;
        public MemberRepository Member
        {
            get
            {
                if (member is null)
                {                    
                    member = new MemberRepository(Connection);
                }
                return member;
            }
        }
        CartRepository cart;
        public CartRepository Cart
        {
            get
            {
                if (cart is null)
                {
                    cart = new CartRepository(Connection);
                }
                return cart;
            }
        }
        ProductRepository product;
        public ProductRepository Product
        {
            get
            {
                if (product is null)
                {
                    product = new ProductRepository(Connection);
                }
                return product;
            }
        }

        FeatureRepository feature;
        public FeatureRepository Feature
        {
            get
            {
                if (feature is null)
                {
                    feature = new FeatureRepository(Connection);
                }
                return feature;
            }
        }
        CarouselRepository carousel;
        public CarouselRepository Carousel
        {
            get
            {
                if (carousel is null)
                {
                    carousel = new CarouselRepository(Connection);
                }
                return carousel;
            }
        }
        SubCategoryRepository subCategory;
        public SubCategoryRepository SubCategory
        {
            get
            {
                if (subCategory is null)
                {
                    subCategory = new SubCategoryRepository(Connection);
                }
                return subCategory;
            }
        }

        CategoryRepository category;
        public CategoryRepository Category
        {
            get
            {
                if (category is null)
                {
                    category = new CategoryRepository(Connection);
                }
                return category;
            }
        }
    }
}
