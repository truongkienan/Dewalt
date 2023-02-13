using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Areas.Dashboard.Controllers
{
    //[Area("dashboard"), Authorize(Roles = "admin")]
    [Area("dashboard")]
    public class ProductController : BaseController
    {
        public ProductController(SiteProvider provider) : base(provider)
        {
        }
        public IActionResult Index()
        {
            return View(provider.Product.GetProducts());
        }

        [HttpPost]
        public IActionResult RemoveVietnameseTone()
        {
            IEnumerable<Product> products = provider.Product.GetProducts();
            List<object> list = new List<object>();
            foreach (var p in products)
            {
                list.Add(new { ProductId = p.ProductId, SearchAnsi = Helper.RemoveVietnameseTone(p.ProductName) });
            }
            provider.Product.RemoveVietnameseTone(list);
            return Redirect("/dashboard/product");
        }
        [HttpPost]
        public IActionResult Edit(Product obj, int id, IFormFile f)
        {
            string fileName = Helper.Upload(f, Const.Products);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }

            obj.ProductId = id;            
            int ret = provider.Product.Edit(obj);
            return Redirect(ret, "Edit Success");
        }

        [ServiceFilter(typeof(ProductStatusFilter))]
        [ServiceFilter(typeof(BrandFilter))]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Edit(int id)
        {
            return View(provider.Product.GetProductById(id));
        }
        public IActionResult Delete(int id)
        {            
            int ret = provider.Product.Delete(id);
            return Redirect(ret, "Delete Success");
        }

        [ServiceFilter(typeof(ProductStatusFilter))]
        [ServiceFilter(typeof(BrandFilter))]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj, IFormFile f)
        {            
            string fileName = Helper.Upload(f, Const.Products);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }            
            int ret = provider.Product.Add(obj);

            return Redirect(ret, "Create Success");
        }

        [HttpPost]
        public IActionResult SubCategories(int id)
        {
            return Json(provider.SubCategory.GetSubCategoriesById(id));
        }
        IActionResult Redirect(int ret, string msg)
        {
            if (ret > 0)
            {
                TempData["msg"] = msg;
                return Redirect("/dashboard/product");
            }
            return Redirect("/dashboard/product/error");
        }
    }
}
