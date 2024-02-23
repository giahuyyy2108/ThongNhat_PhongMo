using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;



        public UserController(HttpClient httpClient, DataBaseContext context,UserManager<User> userManager,SignInManager<User> signInManager) 
        { 
            _httpClient = httpClient;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
			return View(await _context.user.ToListAsync());
		}


        public async Task<IActionResult> logoutUser()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
        // Khởi tạo View Trang thêm người dùng
        public IActionResult Create()
        {
            //View bag lấy ra select Phòng ban
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            return View();
        }

        //xử lý Lưu người dùng
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,hoten,UserName")] User user,int id_phong)
        {
            if (ModelState.IsValid)
            {
                if(user.hoten != null && user.UserName != null)
                {
                    //Mật khẩu mặt định : 123123
                    user.PasswordHash = "AQAAAAEAACcQAAAAEBbxllSqeEJ0nwFoPqDx3V4oNJuPwcoAovSKSXGPVQaao6oERjJHnsh3s+M/f4I00g==";
                    user.EmailConfirmed = true;
                    user.Email = user.UserName + "@example.com";
                    user.NormalizedEmail = user.UserName + "@example.com";
                    user.NormalizedUserName = user.UserName;
                    //lưu thêm phân quyền người dùng tại phòng ban
                    CT_PhongBan phongban = new CT_PhongBan();
                    phongban.Id_User = user.Id;
                    phongban.id_phongban = id_phong;
                    _context.Add(user);
                    _context.Add(phongban);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.erro = "Vui lòng nhấp đầy đủ thông tin";
                }
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            return View(user);
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


        // GET: CT_PhongBan/Edit/5
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _context.user.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            return View(user);
        }

        // POST: CT_PhongBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,hoten,UserName,PasswordHash")] User user, int id_phong)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(user.hoten != null && user.PasswordHash != null)
                    {
                        var nguoidung = await _context.user.FindAsync(id);
                        if (user.PasswordHash != null)
                        {
                            nguoidung.hoten = user.hoten;
                            nguoidung.PasswordHash = HashPassword(user.PasswordHash);
                        }
                        var phong = await _context.CT_PhongBan.FirstOrDefaultAsync(x => x.Id_User == user.Id);
                        if (phong != null)
                        {
                            phong.id_phongban = id_phong;
                            _context.Update(phong);
                        }
                        else
                        {
                            phong = new CT_PhongBan();
                            phong.id_phongban = id_phong;
                            phong.Id_User = user.Id;
                            _context.CT_PhongBan.Add(phong);
                        }
                        _context.Update(nguoidung);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
                        ViewBag.erro = "Vui lòng nhấp đầy đủ thông tin";
                        return View(user);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.erro = "Lỗi trong quá trình lưu";

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            return View(user);
        }

        //Khởi tạo view
        public async Task<IActionResult> EditThongtin(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _context.user.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //Hàm xử lý lưu thay đổi 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditThongtin(string id, [Bind("Id,hoten,UserName,PasswordHash")] User user, string confirmPass,string newpass)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(user.PasswordHash != null && confirmPass !=null)
                    {
                        //Nhập mật khẩu mới và xác nhận mật khẩu trùng nhau
                        if (confirmPass == newpass)
                        {
                            //nhap trùng new & confirm pass
                            var nguoidung = await _context.user.FindAsync(id);
                            if (nguoidung != null)
                            {
                                //thay đổi mật khẩu
                                var result = await _userManager.ChangePasswordAsync(nguoidung, user.PasswordHash, newpass);
                                if (result.Succeeded)
                                {
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                        }
                        else
                        {
                            //sai
                            ViewBag.erro = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                            return View();
                        }
                    }
                    else
                    {
                        //trống
                        ViewBag.erro = "Phải nhập đầy đủ thông tin";
                        return View(user);
                    }

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_phongban"] = new SelectList(_context.phongBan, "Id", "name");
            return View(user);
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
