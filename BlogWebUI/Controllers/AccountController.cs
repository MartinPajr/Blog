using Microsoft.AspNetCore.Mvc;

namespace BlogWebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
