using Dewalt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Controllers
{

    public class ProductController : BaseController
    {

        public ProductController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult Index()
        {            
            return View(provider.Product.GetProducts());
        }     

        [ServiceFilter(typeof(CategoryFilter))]
        [ServiceFilter(typeof(CartFilter))]
        public IActionResult Detail(int id)
        {            
            Product obj = provider.Product.GetProductById(id);
            ViewBag.title = obj.ProductName;
            ViewBag.products = provider.Product.GetRelatedProducts(id, obj.CategoryId);
            return View(obj);
        }

        [ServiceFilter(typeof(CartFilter))]
        [HttpGet("/product/search/{p?}/{s?}")]
        public IActionResult Search(string q, int p = 1, int s=20)
        {            
            IEnumerable<Product> list = provider.Product.SearchProducts(q, p, s, out int totalPage, out int total);
            ViewBag.totalPage = totalPage;
            ViewBag.page = p;
            ViewBag.size = s;
            ViewBag.title = q;
            ViewBag.total = total;
            ViewBag.count = list.Count();
            return View(list);
        }

        [HttpPost]
        public IActionResult _GetProducts(string col, short id, int page = 1, int size = 12, int order = 1, decimal minPrice = 0, decimal maxPrice = 0)
        {
            IEnumerable<Product> list = provider.Product.GetProductsByCategory(col, id, page, size, order, minPrice, maxPrice, out int totalPage, out int total);            
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            ViewBag.total = total;
            return PartialView(list);
        }

        [Authorize(Roles = "Manager")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Category(short id)
        {
            Category obj = provider.Category.GetCategoryById(id);
            ViewBag.title = obj.CategoryName;
            List<decimal> list = provider.Product.GetPricesByCategory(id).ToList();
            return View(list);
        }

        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult SubCategory(short id)
        {                        
            return View(provider.Product.GetPricesBySubCategory(id));
        }
    }
}
