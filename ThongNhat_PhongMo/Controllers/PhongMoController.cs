using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Controllers
{
    public class PhongMoController : Controller
    {
        private readonly DataBaseContext _context;
        public PhongMoController(DataBaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetListPhong()
        {
            var dataBaseContext = await _context.benhnhan
                .Include(p => p.phongban)
                .Include(p => p.tinhtrang)
                .Where(p=> p.tinhtrang.Name != "Done")
                .OrderBy(p=>p.id_phongban)
                .ToListAsync();
            return Json(new { data = dataBaseContext }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
