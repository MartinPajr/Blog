using Listkovac2Auth.Models;
using ListkovacBL.DAO;
using ListkovacDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QRCoder;


namespace Listkovac2Auth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeneralDAO _generalDAO;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _generalDAO = new GeneralDAO();
        }

        public async Task<IActionResult> Index()
        {
            List<ClanekDTO> list = new();
            for (int i = 1; i < 4; i++)
            {
                list.Add(await _generalDAO.GetClanekById(i));
            }


            return View(list);
        }



    }
}