using Listkovac2Auth.Authentication;
using ListkovacBL.DAO;
using ListkovacDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Listkovac2Auth.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGeneralDAO _generalDAO;
        private readonly IJwtProvider _jwtProvider;

        public AccountController(IGeneralDAO generalDAO, IJwtProvider jwtProvider)
        {
            _generalDAO = generalDAO;
            _jwtProvider = jwtProvider;
        }

        public async Task<IActionResult> Login()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            BlogUserDTO user = await _generalDAO.GetByNameUser(username);

            if (user is null)
                return NotFound();

            if (user.Pass != password)
                return NotFound();

            string token = _jwtProvider.Generate(user);

            return Ok(token);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password, string email) 
        //TODO@email varchar(50), @jmeno nvarchar(50), @prijmeni nvarchar(50), @telefon char(12), @cpp nvarchar(6), @Mesto nvarchar(50), @psc char(5), @stat nvarchar(50), @ulice nvarchar(50), @heslo nvarchar(max))
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest();

            //BlogUserDTO existingUser = await _generalDAO.GetByNameUser(username);
            //if (existingUser is not null)
            // return BadRequest();
            BlogUserDTO createdUser = await _generalDAO.CreateBlogUserAsync(username, password, email);

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> PasswordChange()
        {
            return View();   
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange01(string cPassword, string nPassword)
        {

            UserDTO user = await _generalDAO.GetUserbyNameAsync(User.Identity.Name);
            if (user.Pass == cPassword)
            {
                var x = await _generalDAO.UpdatePasswordNameAsync(User.Identity.Name, nPassword);
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
    }
}
