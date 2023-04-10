using Listkovac2Auth.Authentication;
using Listkovac2Auth.Models;
using ListkovacBL.DAO;
using ListkovacDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Listkovac2Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGeneralDAO _generalDAO;
        private readonly IJwtProvider _jwtProvider;

        public AccountController(IGeneralDAO generalDAO, IJwtProvider jwtProvider)
        {
            _generalDAO = generalDAO;
            _jwtProvider = jwtProvider;
        }

        //public async Task<IActionResult> Login()
        //{ 
        //    return View();
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            BlogUserDTO user = await _generalDAO.GetByNameUser(loginRequest.name);

            if (user is null)
                return NotFound();

            if (user.Pass != loginRequest.pass)
                return NotFound();

            string token = _jwtProvider.Generate(user);

            return Ok(token);
        }

        //public async Task<IActionResult> Register()
        //{
        //    return View();
        //}

        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest registerRequest) 
        //TODO@email varchar(50), @jmeno nvarchar(50), @prijmeni nvarchar(50), @telefon char(12), @cpp nvarchar(6), @Mesto nvarchar(50), @psc char(5), @stat nvarchar(50), @ulice nvarchar(50), @heslo nvarchar(max))
        {
            if (string.IsNullOrEmpty(registerRequest.name) || string.IsNullOrEmpty(registerRequest.pass))
                return BadRequest();

            //BlogUserDTO existingUser = await _generalDAO.GetByNameUser(username);
            //if (existingUser is not null)
            // return BadRequest();
            BlogUserDTO createdUser = await _generalDAO.CreateBlogUserAsync(registerRequest.name, registerRequest.pass, registerRequest.email);

            return Ok();
        }
        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
        //public async Task<IActionResult> PasswordChange()
        //{
        //    return View();   
        //}
        //[HttpPost]
        //public async Task<IActionResult> PasswordChange01(string cPassword, string nPassword)
        //{
        //    /*
        //    UserDTO user = await _generalDAO.GetUserbyNameAsync(User.Identity.Name);
        //    if (user.Pass == cPassword)
        //    {
        //        var x = await _generalDAO.UpdatePasswordNameAsync(User.Identity.Name, nPassword);
        //        return RedirectToAction("Index", "Home");
        //    }*/
        //    return View();

        //}
    }
}
