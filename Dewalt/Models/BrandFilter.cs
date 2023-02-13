using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dewalt.Models
{
    public class BrandFilter : Attribute, IActionFilter
    {
        SiteProvider provider;
        public BrandFilter(SiteProvider provider)
        {
            this.provider = provider;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller con)
            {
                con.ViewBag.brands = provider.Brand.GetBrands();
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
