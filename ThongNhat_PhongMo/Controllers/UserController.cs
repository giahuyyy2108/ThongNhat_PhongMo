using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ThongNhat_Hospital;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Controllers
{
    //[Authorize(Roles = "PT-001")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly DataBaseContext _context;

        public UserController(HttpClient httpClient, DataBaseContext context) 
        { 
            _httpClient = httpClient;
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            ViewData["id_tinhtrang"] = new SelectList(_context.tinhtrang, "Id", "Name");
            return PartialView("index", new ThongTinKhamBenh());
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("mabn,hoten,namsinh,id_tinhtrang")] ThongTinKhamBenh Thongtin)
        {

            Thongtin.id_user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Thongtin.id_phongban = Int32.Parse(User.FindFirstValue(ClaimTypes.Name).Substring(7));
            Thongtin.id_tinhtrang = 1;
            Thongtin.id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _context.Add(Thongtin);
                await _context.SaveChangesAsync();
                return View(Thongtin);
            }
            ViewData["id_tinhtrang"] = new SelectList((System.Collections.IEnumerable)Thongtin.tinhtrang, "Id", "Name");
            return View(Thongtin);
        }
        // GET: LoaiHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHang = await _context.benhnhan.FindAsync(id);
            if (loaiHang == null)
            {
                return NotFound();
            }
            return PartialView(loaiHang);
        }

        // POST: LoaiHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_tinhtrang")] ThongTinKhamBenh benhnhan)
        {
            if (id != benhnhan.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    benhnhan.id_tinhtrang++;
                    _context.Update(benhnhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!LoaiHangExists(loaiHang.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Edit", _context.benhnhan.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", benhnhan) });
        }

        public async Task<IActionResult> getBNbyId(string mabn)
        {
            string apiUrl = $"https://hsoftapi.bvtn.org.vn/api/upd_hsoft_benhnhan/?ip=192.168.0.75&idbv=79025&mabn={mabn}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throw an exception if the request wasn't successful

                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
