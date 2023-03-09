using Dewalt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dewalt.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(SiteProvider provider) : base(provider)
        {
        }

        [ServiceFilter(typeof(CategoryFilter))]
        [ServiceFilter(typeof(CartFilter))]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel obj, string returnUrl = "/")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Member member = provider.Member.Login(obj);
                    if (member != null)
                    {
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, obj.Username),
                            new Claim(ClaimTypes.Email, member.Email),
                            new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                        AuthenticationProperties properties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(principal, properties);
                        return Redirect(returnUrl);
                    }
                    TempData["msg"] = "Login Failed";
                    return View(obj);
                }
                return View(obj);
            }
            catch
            {
                return BadRequest();
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
