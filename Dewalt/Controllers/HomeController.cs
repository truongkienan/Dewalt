using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult _GetProductAndFeatures(short id)
        {
            IEnumerable<ProductAndFeature> productAndFeatures = provider.Product.GetProductAndFeatures();
            Dictionary<short, List<Product>> dict = new Dictionary<short, List<Product>>();
            foreach (var item in productAndFeatures)
            {
                short k = item.FeatureId;
                if (!dict.ContainsKey(k))
                {
                    dict[k] = new List<Product>();
                }
                dict[k].Add(item);
            }
            return PartialView(dict[id]);
        }

        [ServiceFilter(typeof(CategoryFilter))]
        [ServiceFilter(typeof(CartFilter))]
        public IActionResult Index()
        {            
            IEnumerable<ProductAndFeature> productAndFeatures = provider.Product.GetProductAndFeatures();
            Dictionary<short, List<Product>> dict = new Dictionary<short, List<Product>>();
            foreach (var item in productAndFeatures)
            {
                short k = item.FeatureId;
                if (!dict.ContainsKey(k))
                {
                    dict[k] = new List<Product>();
                }
                dict[k].Add(item);
            }
            ViewBag.productAndFeatures = dict;
            ViewBag.features = provider.Feature.GetFeatures();
            ViewBag.bestSellers = provider.Product.GetBestSellers();
            ViewBag.newArrivals = provider.Product.GetNewArrivals();
            ViewBag.carousels = provider.Carousel.GetCarousels();
            return View();
        }
        [ServiceFilter(typeof(CartFilter))]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Message obj)
        {
            try
            {
                provider.Contact.Contact(obj);
                TempData["msg"] = "Send Succes";
                return RedirectToAction("contact");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["msg"] = $"Send Email Failed: {ex.Message}";
                return RedirectToAction("error");
            }
        }
    }
}
