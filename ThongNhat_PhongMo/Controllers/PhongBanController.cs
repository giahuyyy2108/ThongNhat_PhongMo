using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Controllers
{
    public class PhongBanController : Controller
    {
        private readonly DataBaseContext _context;

        public PhongBanController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: PhongBan
        public async Task<IActionResult> Index()
        {
            return View(await _context.phongBan.ToListAsync());
        }

        // GET: PhongBan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongBan = await _context.phongBan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phongBan == null)
            {
                return NotFound();
            }

            return View(phongBan);
        }

        //Khởi tạo view
        public IActionResult Create()
        {
            return View();
        }
        //Hàm lưu Phòng ban
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,sceensize,time")] PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phongBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phongBan);
        }

        //Khởi tạo view Edit Phòng ban
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongBan = await _context.phongBan.FindAsync(id);
            if (phongBan == null)
            {
                return NotFound();
            }
            return View(phongBan);
        }

        //Hàm xử lý lưu chỉnh sửa phòng ban
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,sceensize,time")] PhongBan phongBan)
        {
            if (id != phongBan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongBanExists(phongBan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(phongBan);
        }

        // Khởi tạo trang xóa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongBan = await _context.phongBan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phongBan == null)
            {
                return NotFound();
            }

            return View(phongBan);
        }

        // Hàm xóa Phòng ban
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phongBan = await _context.phongBan.FindAsync(id);
            _context.phongBan.Remove(phongBan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongBanExists(int id)
        {
            return _context.phongBan.Any(e => e.Id == id);
        }
    }
}
