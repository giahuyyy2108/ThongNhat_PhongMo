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
    public class CT_PhongBanController : Controller
    {
        private readonly DataBaseContext _context;

        public CT_PhongBanController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: CT_PhongBan
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.CT_PhongBan
                .Include(c => c.phong)
                .Include(c => c.user);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: CT_PhongBan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cT_PhongBan = await _context.CT_PhongBan
                .Include(c => c.phong)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cT_PhongBan == null)
            {
                return NotFound();
            }

            return View(cT_PhongBan);
        }

        // GET: CT_PhongBan/Create
        public IActionResult Create()
        {
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            ViewData["Id_User"] = new SelectList(_context.user.Where(p=>p.UserName != "admin").Where(c => c.NormalizedUserName != null), "Id", "UserName");
            return View();
        }

        // POST: CT_PhongBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Id_User,id_phongban")] CT_PhongBan cT_PhongBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cT_PhongBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            ViewData["Id_User"] = new SelectList(_context.user.Where(p => p.UserName != "admin").Where(c => c.NormalizedUserName != null), "Id", "UserName");
            return View(cT_PhongBan);
        }

        // GET: CT_PhongBan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cT_PhongBan = await _context.CT_PhongBan.FindAsync(id);
            if (cT_PhongBan == null)
            {
                return NotFound();
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            ViewData["Id_User"] = new SelectList(_context.user.Where(p => p.UserName != "admin").Where(c => c.NormalizedUserName != null), "Id", "UserName");
            return View(cT_PhongBan);
        }

        // POST: CT_PhongBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Id_User,id_phongban")] CT_PhongBan cT_PhongBan)
        {
            if (id != cT_PhongBan.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cT_PhongBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CT_PhongBanExists(cT_PhongBan.id))
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
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            ViewData["Id_User"] = new SelectList(_context.user.Where(p => p.UserName != "admin").Where(c => c.NormalizedUserName != null), "Id", "UserName");
            return View(cT_PhongBan);
        }

        // GET: CT_PhongBan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cT_PhongBan = await _context.CT_PhongBan
                .Include(c => c.phong)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cT_PhongBan == null)
            {
                return NotFound();
            }

            return View(cT_PhongBan);
        }

        // POST: CT_PhongBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cT_PhongBan = await _context.CT_PhongBan.FindAsync(id);
            _context.CT_PhongBan.Remove(cT_PhongBan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CT_PhongBanExists(int id)
        {
            return _context.CT_PhongBan.Any(e => e.id == id);
        }
    }
}
