using Dewalt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Dewalt.Controllers
{
    public class CartController : BaseController
    {
        const string CartCode = "cart";

        public CartController(SiteProvider provider) : base(provider)
        {
        }

        [ServiceFilter(typeof(CartFilter))]
        [ServiceFilter(typeof(CategoryFilter))]
        [HttpPost]
        [Authorize]
        public IActionResult Address(Address obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.MemberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    provider.Address.Add(obj);
                    return Redirect("/cart/checkout");
                }
                ViewBag.valid = 0;
                Address();
                ViewBag.provinces = new SelectList(provider.Province.GetProvinces(), "ProvinceId", "ProvinceName");
                string code = Request.Cookies[CartCode];
                IEnumerable<Cart> list = provider.Cart.GetCarts(code, out decimal total);
                Tuple<IEnumerable<Cart>, decimal> tuple = new Tuple<IEnumerable<Cart>, decimal>(list, total);
                return View("Checkout", tuple);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult District(byte id)
        {
            return Json(provider.District.GetDistricts(id));
        }
        [HttpPost]
        public IActionResult Ward(short id)
        {
            return Json(provider.Ward.GetWards(id));
        }

        [ServiceFilter(typeof(CategoryFilter))]
        [ServiceFilter(typeof(CartFilter))]
        [Authorize]
        public IActionResult Checkout()
        {
            try
            {
                string code = Request.Cookies[CartCode];
                Address();
                ViewBag.valid = 1;
                ViewBag.provinces = new SelectList(provider.Province.GetProvinces(), "ProvinceId", "ProvinceName");
                IEnumerable<Cart> list = provider.Cart.GetCarts(code, out decimal total);
                if (list != null)
                {
                    Tuple<IEnumerable<Cart>, decimal> tuple = new Tuple<IEnumerable<Cart>, decimal>(list, total);
                    return View(tuple);
                }
                return Redirect("/");
            }
            catch
            {
                return BadRequest();
            }
        }


        [Authorize, HttpPost]
        public IActionResult Checkout(Invoice obj)
        {
            try
            {
                string code = Request.Cookies[CartCode];
                obj.CartCode = code;
                obj.MemberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                provider.Invoice.Add(obj);
                return Redirect($"/invoice/detail/{obj.InvoiceId}");
            }
            catch
            {
                return BadRequest();
            }
        }

        [ServiceFilter(typeof(CategoryFilter))]
        [ServiceFilter(typeof(CartFilter))]
        public IActionResult Index()
        {
            try
            {
                string code = Request.Cookies[CartCode];
                if (!string.IsNullOrEmpty(code))
                {
                    IEnumerable<Cart> list = provider.Cart.GetCarts(code, out decimal total);
                    Tuple<IEnumerable<Cart>, decimal> tuple = new Tuple<IEnumerable<Cart>, decimal>(list, total);
                    return View(tuple);
                }

                return Redirect("/");
            }
            catch
            {
                return BadRequest();
            }
        }
        public IActionResult Add(Cart obj)
        {
            try
            {
                string code = Request.Cookies[CartCode];
                if (string.IsNullOrEmpty(code))
                {
                    code = Helper.RandomString(32);
                    Response.Cookies.Append(CartCode, code);
                }
                obj.CartCode = code;
                provider.Cart.Save(obj);
                return Redirect("/cart");
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public IActionResult Edit(Cart obj)
        {
            try
            {
                obj.CartCode = Request.Cookies[CartCode];
                return Json(provider.Cart.Edit(obj));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Delete(Cart obj)
        {
            try
            {
                obj.CartCode = Request.Cookies[CartCode];
                return Json(provider.Cart.Delete(obj));
            }
            catch
            {
                return BadRequest();
            }
        }

        void Address()
        {
            
            ViewBag.addresses = provider.Address.GetAddresses(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
