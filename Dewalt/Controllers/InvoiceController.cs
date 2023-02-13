using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Controllers
{
    public class InvoiceController : BaseController
    {
        public InvoiceController(SiteProvider provider) : base(provider)
        {
        }

        [ServiceFilter(typeof(CartFilter))]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Detail(string id)
        {
            Invoice inv = provider.Invoice.GetInvoiceById(id);
            return View(provider.Invoice.GetInvoiceById(id));
        }
    }
}
