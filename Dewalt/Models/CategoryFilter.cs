using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dewalt.Models
{
    public class CategoryFilter : Attribute, IActionFilter
    {
        SiteProvider provider;
        public CategoryFilter(SiteProvider provider)
        {
            this.provider = provider;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller con)
            {
                IEnumerable<SubCategory> subCategories = provider.SubCategory.GetSubCategories();
                Dictionary<short, List<SubCategory>[]> dictSub = new Dictionary<short, List<SubCategory>[]>();
                foreach (SubCategory item in subCategories)
                {
                    short k = item.CategoryId;
                    if (!dictSub.ContainsKey(k))
                    {
                        dictSub[k] = new List<SubCategory>[3];
                        for (int i = 0; i < 3; i++)
                        {
                            dictSub[k][i] = new List<SubCategory>();
                        }
                    }
                    dictSub[k][item.ColumnIndex].Add(item);
                }
                con.ViewBag.dictSub = dictSub;
                con.ViewBag.categories = provider.Category.GetCategories();                
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
