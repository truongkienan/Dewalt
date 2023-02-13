using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dewalt.Controllers
{
    public abstract class BaseController : Controller
    {

        protected SiteProvider provider;
        public BaseController(SiteProvider provider)
        {
            this.provider = provider;
        }
    }
}
