using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class BrandController: BaseController
    {
        public BrandController(SiteProvider provider) : base(provider)
        {
        }

        //[Authorize(AuthenticationSchemes = "dashboard")]
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            return View(provider.Brand.GetBrands());
        }
        public IActionResult Edit(byte id)
        {            
            return View(provider.Brand.GetBrandById(id));
        }
        [HttpPost]
        public IActionResult Edit(byte id, Brand obj, IFormFile f)
        {
            string fileName = Helper.Upload(f, Const.Brands);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }

            obj.BrandId = id;       
            int ret = provider.Brand.Edit(obj);
            if (ret > 0)
            {
                TempData["msg"] = "Edit Success";
                return Redirect("/dashboard/brand");
            }
            return Redirect("/dashboard/brand/error");
        }

        public IActionResult Delete(byte id)
        {
            int ret = provider.Brand.Delete(id);

            if (ret > 0)
            {
                TempData["msg"] = "Delete Success";
                return Redirect("/dashboard/brand");
            }
            return Redirect("/dashboard/brand/error");
        }

        [HttpPost]
        public IActionResult DeleteMultiple(List<byte> arr)
        {            
            if (arr.Count == 0)
            {
                TempData["msg"] = "Please select items";
                return Redirect("/dashboard/brand");
            }

            int ret = provider.Brand.DeleteMultiple(arr);

            if (ret > 0)
            {
                TempData["msg"] = "Delete Success";
                return Redirect("/dashboard/brand");
            }
            return Redirect("/dashboard/brand/error");
        }
        [HttpPost]
        public IActionResult Create(Brand obj, IFormFile f)
        {
            string? fileName = Helper.Upload(f, Const.Brands);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }

            int ret = provider.Brand.Add(obj);

            if (ret > 0)
            {
                TempData["msg"] = "Add Success";
                return Redirect("/dashboard/brand");
            }
            return Redirect("/dashboard/brand/error");
        }
    }
}
