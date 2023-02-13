using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class RoleController : BaseController
    {
        public RoleController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult Index()
        {
            return View(provider.Role.GetRoles());
        }
        [HttpPost]
        public IActionResult Create(Role obj)
        {
            int ret = provider.Role.Add(obj);
            return Redirect("/dashboard/role");
        }
    }
}
