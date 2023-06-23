﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Controllers
{
    [Authorize(Roles ="PT-001")]
    public class ThongTinKhamBenhsController : Controller
    {
        private readonly DataBaseContext _context;

        public ThongTinKhamBenhsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: ThongTinKhamBenhs
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.benhnhan
                                    .Include(t => t.phongban)
                                    .Include(t => t.tinhtrang)
                                    .Include(t => t.user)
                                    .Where(t => t.id_phongban == Int32.Parse(User.FindFirstValue(ClaimTypes.Name).Substring(7)))
                                    .OrderBy(t=>t.Thoigian);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: ThongTinKhamBenhs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongTinKhamBenh = await _context.benhnhan
                .Include(t => t.phongban)
                .Include(t => t.tinhtrang)
                .Include(t => t.user)
                .Where(t=>t.id_user == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync(m => m.id == id);
            if (thongTinKhamBenh == null)
            {
                return NotFound();
            }

            return View(thongTinKhamBenh);
        }

        // GET: ThongTinKhamBenhs/Create
        public IActionResult Create()
        {
            ViewData["id_tinhtrang"] = new SelectList(_context.tinhtrang, "id", "Name");
            ViewData["id_user"] = new SelectList(_context.user, "Id", "Id");
            ThongTinKhamBenh modle = new ThongTinKhamBenh()
            {
                ThoigianDuKien = DateTime.Parse((DateTime.Now.AddMinutes(20)).ToShortTimeString())
            };
            return View(modle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("hoten,namsinh,mabn,ThoigianDuKien")] ThongTinKhamBenh thongTinKhamBenh)
        {
            thongTinKhamBenh.id_tinhtrang = 1;
            thongTinKhamBenh.id_phongban = Int32.Parse(User.FindFirstValue(ClaimTypes.Name).Substring(7));
            thongTinKhamBenh.id = Guid.NewGuid().ToString();
            thongTinKhamBenh.id_user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            thongTinKhamBenh.Thoigian = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (checkbn(thongTinKhamBenh) == true)
                {
                    ViewBag.erro = "Bệnh nhân đang có trong danh sách";
                    return View(thongTinKhamBenh);
                }
                _context.Add(thongTinKhamBenh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "Id", thongTinKhamBenh.id_phongban);
            ViewData["id_tinhtrang"] = new SelectList(_context.tinhtrang, "id", "Name", thongTinKhamBenh.id_tinhtrang);
            ViewData["id_user"] = new SelectList(_context.user, "Id", "Id", thongTinKhamBenh.id_user);
            return View(thongTinKhamBenh);
        }

        // GET: ThongTinKhamBenhs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongTinKhamBenh = await _context.benhnhan.FindAsync(id);
            if (thongTinKhamBenh == null)
            {
                return NotFound();
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name", thongTinKhamBenh.id_phongban);
            ViewData["id_tinhtrang"] = new SelectList(_context.tinhtrang, "id", "Name", thongTinKhamBenh.id_tinhtrang);
            ViewData["id_user"] = new SelectList(_context.user, "Id", "Id", thongTinKhamBenh.id_user);
            return View(thongTinKhamBenh);
        }

        // POST: ThongTinKhamBenhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,hoten,namsinh,mabn,id_tinhtrang,id_phongban,Thoigian,ThoigianDuKien")] ThongTinKhamBenh thongTinKhamBenh)
        {
            if (id != thongTinKhamBenh.id)
            {
                return NotFound();
            }
            thongTinKhamBenh.id_user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongTinKhamBenh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongTinKhamBenhExists(thongTinKhamBenh.id))
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
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "Id", thongTinKhamBenh.id_phongban);
            ViewData["id_tinhtrang"] = new SelectList(_context.tinhtrang, "id", "id", thongTinKhamBenh.id_tinhtrang);
            ViewData["id_user"] = new SelectList(_context.user, "Id", "Id", thongTinKhamBenh.id_user);
            return View(thongTinKhamBenh);
        }

        // GET: ThongTinKhamBenhs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongTinKhamBenh = await _context.benhnhan
                .Include(t => t.phongban)
                .Include(t => t.tinhtrang)
                .Include(t => t.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (thongTinKhamBenh == null)
            {
                return NotFound();
            }

            return View(thongTinKhamBenh);
        }

        // POST: ThongTinKhamBenhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var thongTinKhamBenh = await _context.benhnhan.FindAsync(id);
            _context.benhnhan.Remove(thongTinKhamBenh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongTinKhamBenhExists(string id)
        {
            return _context.benhnhan.Any(e => e.id == id);
        }

        public async Task<IActionResult> Update(string id)
        {
            var thongTinKhamBenh = await _context.benhnhan.FindAsync(id);
            thongTinKhamBenh.id_tinhtrang++;
            _context.benhnhan.Update(thongTinKhamBenh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public bool checkbn(ThongTinKhamBenh benhnhan)
        {
            var data = _context.benhnhan
                                    .Include(t => t.phongban)
                                    .Include(t => t.tinhtrang)
                                    .Include(t => t.user)
                                    .Where(t=> t.id_tinhtrang < 5)
                                    .ToList();
            foreach (var item in data)
            {
                if(benhnhan.mabn == item.mabn)
                    return true;
            }
            return false;
        }
    }
}