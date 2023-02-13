using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class HomeController : BaseController
    {
        public HomeController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult Index()
        {
            return View(provider.Invoice.StatisticInvoices());
        }
    }
}
