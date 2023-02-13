using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class ProvinceController : BaseController
    {
        public ProvinceController(SiteProvider provider) : base(provider)
        {
        }

        public IActionResult Index()
        {
            return View(provider.Province.GetProvinces());
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import(IFormFile f)
        {
            if (f != null)
            {
                IWorkbook workbook = new XSSFWorkbook(f.OpenReadStream());
                ISheet sheet = workbook.GetSheetAt(0);
                List<Province> list = new List<Province>();
                for (int i = 1; i < sheet.LastRowNum; i++)
                {
                    IRow item = sheet.GetRow(i);
                    Province province = new Province
                    {
                        ProvinceId = Convert.ToByte(item.GetCell(3).NumericCellValue),
                        ProvinceName = item.GetCell(4).StringCellValue,
                        ProvinceType = item.GetCell(5).StringCellValue
                    };
                    list.Add(province);
                }
                provider.Province.Add(list);
                return Redirect("/dashboard/province");                
            }
            return View();
        }
    }
}
