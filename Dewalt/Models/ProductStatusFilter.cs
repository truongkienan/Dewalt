using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dewalt.Models
{
    public class ProductStatusFilter : Attribute, IActionFilter
    {
        SiteProvider provider;
        public ProductStatusFilter(SiteProvider provider)
        {
            this.provider = provider;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller con)
            {
                con.ViewBag.productStatus = provider.Product.GetStatus();
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
