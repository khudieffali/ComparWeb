using AutoMapper;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Identity.Controllers
{
    [Area(nameof(Identity))]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user == null) return View();
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (!result.Succeeded)
            {
                return View();  
            }

            return RedirectToAction("Index", "Dashboard", new {area="ComparAdmin"});
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                if (registerDTO.Password != registerDTO.ConfirmPassword)
                {
                    ViewBag.Password = "Şifrələr eyni deyil";
                    return View();
                }
                var user = _mapper.Map<User>(registerDTO);
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (!result.Succeeded)
                {
                    return View();
                }
                await _userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("Login");
            }
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = " " });
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string? id)
        {
           
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string? Id,string currentPassword, string newPassword,string newConfirmPassword)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (newPassword != newConfirmPassword)
            {
                ViewBag.Password = "Şifrələr eyni deyil";
                return View(user);
            }
            await _userManager.ChangePasswordAsync(user,currentPassword,newPassword);
            return RedirectToAction("Index", "Dashboard", new { area = "ComparAdmin" });
        }
    }
}
