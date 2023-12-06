using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
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
        // API Nhan tat ca thong tin benh nhan cac phong
		public async Task<IActionResult> GetAllListPhongMo()
		{
			var dataBaseContext = await _context.benhnhan
				.Include(p => p.phongban)
				.Include(p => p.tinhtrang)
				.Where(p => p.tinhtrang.Name != "Done")
				.Where(p => p.Thoigian.Date == DateTime.Now.Date)
				.OrderBy(t=>t.Thoigian)
				.ToListAsync();
			return Json(new { data = dataBaseContext }, new Newtonsoft.Json.JsonSerializerSettings
			                                                                            {
				                                                                            DateFormatString = "HH:mm",
				                                                                            DateTimeZoneHandling = DateTimeZoneHandling.Utc
			                                                                            });
		}
		// API Nhan thong tin benh nhan tai phong

		public async Task<IActionResult> Phong(int id)
        {
            var dataBaseContext = await _context.benhnhan
                .Include(p => p.phongban)
                .Include(p => p.tinhtrang)
                .Where(p => p.tinhtrang.Name != "Done")
                .Where(p => p.Thoigian.Date == DateTime.Now.Date)
                .Where(p => p.id_phongban == id)
                .OrderBy(t => t.Thoigian)
                .ToListAsync();
            PhongViewModel data = new PhongViewModel();
            data.phong = await _context.phongBan.Where(p => p.Id == id).FirstOrDefaultAsync();
            foreach (var item in dataBaseContext)
            {
                data.benhnhan = item;
            }
            return View(data);
        }

        public async Task<IActionResult> GetListPhong(int phong)
        {
            var dataBaseContext = await _context.benhnhan
                .Include(p => p.phongban)
                .Include(p => p.tinhtrang)
                .Where(p=> p.tinhtrang.Name != "Done")
				.Where(p => p.Thoigian.Date == DateTime.Now.Date)
				.Where(p=> p.id_phongban == phong)
				.OrderBy(t => t.Thoigian)
                .ToListAsync();

            return Json(new { data = dataBaseContext }, new Newtonsoft.Json.JsonSerializerSettings() 
                                                                                {
				                                                                  DateFormatString = "HH:mm", 
                                                                                  DateTimeZoneHandling = DateTimeZoneHandling.Utc 
                                                                                });
        }
    }
}
