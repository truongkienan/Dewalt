using Dewalt.Controllers;
using Dewalt.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Dewalt.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class DistrictController : BaseController
    {
        public DistrictController(SiteProvider provider) : base(provider)
        {
        }
        public IActionResult Index()
        {
            return View(provider.District.GetDistricts());
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
                List<District> list = new List<District>();
                for (int i = 1; i < sheet.LastRowNum; i++)
                {
                    IRow item = sheet.GetRow(i);
                    District district = new District
                    {
                        DistrictId = Convert.ToInt16(item.GetCell(5).NumericCellValue),
                        ProvinceId = Convert.ToByte(item.GetCell(3).NumericCellValue),
                        DistrictName = item.GetCell(6).StringCellValue,
                        DistrictType = item.GetCell(7).StringCellValue
                    };
                    list.Add(district);
                }
                provider.District.Add(list);
                return Redirect("/dashboard/district");                
            }
            return View();
        }
    }
}
