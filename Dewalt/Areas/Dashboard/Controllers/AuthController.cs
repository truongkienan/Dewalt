using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class AuthController : BaseController
    {

        public AuthController(SiteProvider provider) : base(provider)
        {
        }

        //AccountRepository account = new AccountRepository();
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult Change()
        {
            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Change(ChangeAccModel obj)
        {
            obj.Username = User.Identity.Name;            
            int ret = provider.Member.Change(obj);
            if (ret > 0)
            {
                await HttpContext.SignOutAsync("AccountCookies");
                return Redirect("/dashboard/auth/login");
            }
            ModelState.AddModelError(string.Empty, "Old Password Failed");
            return View(obj);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/auth/login");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Member member = provider.Member.Login(obj);
                    if (member != null)
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, member.Username),
                            new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
                            new Claim(ClaimTypes.Email, member.Email),
                            new Claim(ClaimTypes.DateOfBirth, member.DateOfBirth.ToString("dd/MM/yyyyy")),
                            new Claim(ClaimTypes.Gender, member.Gender.ToString()),
                        };

                        IEnumerable<Role> roles = provider.Role.GetRolesByMember(member.MemberId);
                        if (roles != null)
                        {
                            foreach (Role item in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                            }
                        }

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            IsPersistent = obj.Remember
                        };

                        await HttpContext.SignInAsync(principal, properties);
                        return Redirect("/dashboard");
                    }
                    ModelState.AddModelError(String.Empty, "Login Failed");
                    return View(obj);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return View(obj);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Member obj)
        {
            int ret=-1;
            if (ModelState.IsValid)
            {
                ret = provider.Member.Add(obj);
                if (ret > 0)
                {
                    return Redirect("/auth/login");
                }
            }

        string[] err = { "Username exists", "Add Member Failed" };

            ModelState.AddModelError(string.Empty, err[ret + 1]);
            return View(obj);
        }
    }
}
