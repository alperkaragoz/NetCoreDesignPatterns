using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager)
        {
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Not Found!");
            }

            // lockoutOnFailure -> Identity Api içerisinde built-in bir mekanizma geliyor.Örn: Kullanıcı şifreyi yanlış girince 20dk kullanıcıyı kilitliyor.Bunu istemiyor isek lockoutOnFailure=false yapıyoruz.
            var signInResult = await _singInManager.PasswordSignInAsync(user, password, true, false);

            if (!signInResult.Succeeded)
            {
                return View();
            }

            // Index'in ismi değişirse uygulama Compile anında hata fırlatsın diye "nameof" keywordünü kullanıyoruz.
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
