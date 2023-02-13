using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class MemberController : BaseController
    {
        public MemberController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult Index()
        {
            return View(provider.Member.GetMembers());
        }
        public IActionResult Roles(Guid id)
        {
            ViewBag.member = provider.Member.GetMemberById(id);
            return View(provider.Role.GetRolesCheckedByMember(id));
        }
        [HttpPost]
        public IActionResult Roles(MemberInRole obj)
        {
            return Json(provider.MemberInRole.Save(obj));
        }
    }
}
