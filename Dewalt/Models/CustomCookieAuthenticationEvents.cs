using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dewalt.Models
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public static bool IsAdminContext(RedirectContext<CookieAuthenticationOptions> context)
        {
            return context.Request.Path.StartsWithSegments("/dashboard");
        }
    }
}
