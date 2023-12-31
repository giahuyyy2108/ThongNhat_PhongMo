﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<User> signInManager,
            ILogger<LoginModel> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Phải nhập tên đăng nhập")]
            //[EmailAddress]
            [Display(Name ="Tài khoản")]
            public string EmailOrUserName { get; set; }
            [Display(Name = "Mật khẩu")]

            [Required(ErrorMessage = "Phải nhập mật khẩu")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.EmailOrUserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(Input.EmailOrUserName);
                    if (user != null)
                    {
                        result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    }
                }


                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.EmailOrUserName);
                    if (user != null)
                    {
                        var rolename = await _userManager.GetRolesAsync(user);
                        foreach (var item in rolename)
                        {
                            if (item.Equals("admin"))
                            {
                                _logger.LogInformation("User logged in.");
                                return LocalRedirect("/admin/");
                            }
                            if (item.Equals("PT-001"))
                            {
                                _logger.LogInformation("User logged in.");
                                return LocalRedirect("/ThongTinKhamBenhs/");
                            }
                        }
                    }
                    else
                    {
                        var emailuser = await _userManager.FindByEmailAsync(Input.EmailOrUserName);
                        var user1 = await _userManager.FindByNameAsync(emailuser.UserName);
                        var rolename2 = await _userManager.GetRolesAsync(user1);
                        foreach (var item in rolename2)
                        {
                            if (item.Equals("admin"))
                            {
                                _logger.LogInformation("User logged in.");
                                return LocalRedirect("/admin/");
                            }
                            if (item.Equals("PT-001"))
                            {
                                _logger.LogInformation("User logged in.");
                                return LocalRedirect("/user/index");
                            }
                        }
                    }


                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sai thông thông tin đăng nhập hoặc mật khẩu");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
