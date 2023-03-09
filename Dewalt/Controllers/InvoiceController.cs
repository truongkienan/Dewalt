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
            try
            {
                Invoice inv = provider.Invoice.GetInvoiceById(id);
                if (inv == null)
                {
                    return NotFound();
                }
                return View(inv);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
