using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class InvoiceController : BaseController
    {
        public InvoiceController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult Index()
        {
            ViewBag.statuses = new SelectList(provider.Status.GetStatuses(), "StatusId","StatusName");            
            return View(provider.Invoice.GetInvoices());
        }
        [HttpPost]
        public IActionResult Edit(Invoice obj)
        {
            provider.Invoice.UpdateStatus(obj);
            return Redirect("/dashboard/invoice");
        }

        [HttpPost]
        public IActionResult Detail(string id)
        {
            return Json(provider.Invoice.GetInvoiceById(id));
        }
    }
}
