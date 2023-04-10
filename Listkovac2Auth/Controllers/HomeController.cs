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
        [HttpGet]
        public async Task<IActionResult> Article(int Id)
        {

            SingleClanekDTO clanek = new();
            clanek.Clanek = await _generalDAO.GetClanekById(Id);
            List<KomentarDTO> komenty = await _generalDAO.GetKomentareKClanku(Id);
            foreach (KomentarDTO komentar in komenty)
            {
                komentar.User = await _generalDAO.GetUser(komentar.UserId);
            }
            clanek.Komentare = komenty;

            return View(clanek);
        }
        [HttpPost][HttpGet]
        public async Task<IActionResult> CreateNewComent(KomentarDTO koment, int id)
        {
            KomentarDTO newKoment = new();

            BlogUserDTO uzivatel = new();
            uzivatel = await _generalDAO.GetUserNameAsync(User.Identity.Name);
            newKoment.User = uzivatel;
            newKoment.Text = koment.Text;
            newKoment.ClanekId = id;
            newKoment.Time = DateTime.Now;

            var y = await _generalDAO.CreateNewComentAsync(newKoment);

            return RedirectToAction("/Home/Article?Id=@id");
        }









        [HttpGet]
        public async Task<IActionResult> Akce(int Id)
        {
            SingleFullVenueDTO singleVenue = new();

            List<TicketsAkceDTO> vstupenkySerie = await _generalDAO.GetVstupenkyByAkceID(Id);
            singleVenue.Tickety = vstupenkySerie;

            AkceDTO akce = await _generalDAO.GetAkceByIdAsync(Id);
            singleVenue.Akce = akce;

            AddressDTO adresa = await _generalDAO.GetAddressById(akce.Prostory_konaniID);
            singleVenue.Adresa = adresa;

            VenueDTO misto = await _generalDAO.GetVenueById(akce.Prostory_konaniID);
            singleVenue.Misto = misto;
            

            return View(singleVenue);
        }
        [HttpPost]
        public async Task<IActionResult> BuyTickets(BuyTicketsDTO order)
        {

            UserDTO uzivatel = await _generalDAO.GetUserbyNameAsync(User.Identity.Name);
            if(uzivatel == null) { return RedirectToAction("Index", "Home"); }

            var x = await _generalDAO.ObjednaniVstupenekPROCEDURE(order,uzivatel.Username,uzivatel.Pass);

            return RedirectToAction("UserMenu", "Home");
        }
        public async Task<IActionResult> Upcoming()
        {
            List<AkceFullDTO> list = new();
            list = await _generalDAO.GetAkceByTimeAsync();
            return View(list);
        }
        public async Task<IActionResult> Past()
        {
            List<AkceFullDTO> list = new();
            list = await _generalDAO.GetAkceByTimeAsync();
            return View(list);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> UserMenu()
        {
            /*
            UserMenuDTO model = new UserMenuDTO();

            List<VenueDTO> list = new();
            list = await _generalDAO.GetAllVenuesAsync();
            model.Venues = list;
            List<AkceFullDTO> list2 = new();
            list2 = await _generalDAO.GetAkceByTimeAsync();
            model.Akce = list2;
            UserDTO uzivatel = new();
            uzivatel = await _generalDAO.GetUserbyNameAsync(User.Identity.Name);
            model.PoradatelID = uzivatel.IdPoradatele;
            model.Role = uzivatel.Role;
            model.Zakaznik = await _generalDAO.GetZakaznikInfo(uzivatel.IdZakaznika);
            */

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewAkce(CreateAkceDTO createAkceDTO)
        {
            AkceFullDTO fullko = new();

            fullko.Datum = Convert.ToDateTime(createAkceDTO.Date);
            fullko.Nazevakce = createAkceDTO.Name;
            fullko.Prostory_konaniID = createAkceDTO.ProstoryID;
            fullko.Popisakce = createAkceDTO.Popisakce;
            UserDTO uzivatel = new();
            uzivatel = await _generalDAO.GetUserbyNameAsync(User.Identity.Name);
            fullko.PoradateleID = uzivatel.IdPoradatele;
            var x = await _generalDAO.CreateNewAkceAsync(fullko);

            return RedirectToAction("UserMenu", "Home");
        }

        public async Task<IActionResult> MyTickets()
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            UserDTO uzivatel = new();
            uzivatel = await _generalDAO.GetUserbyNameAsync(User.Identity.Name);
            
            List<TicketDTO> myTickets = await _generalDAO.GetVstupenkyByUser(uzivatel.IdZakaznika);
            foreach (TicketDTO ticket in myTickets)
            {
                TicketDTO qrHandle = await _generalDAO.QrHandler(ticket);
                ticket.QrCode = qrHandle.QrCode;
                AkceDTO akcicka = await _generalDAO.GetAkceByIdAsync(ticket.AkceID);
                ticket.NazevAkce = akcicka.Nazevakce;
                if (akcicka.Datum > DateTime.Now)
                {
                    ticket.Status = true;
                }
                else
                {
                    ticket.Status = false;
                }
                QRCodeData data = generator.CreateQrCode(ticket.QrCode, QRCodeGenerator.ECCLevel.Q);
                ticket.QrImage = new BitmapByteQRCode(data);
            }

            return View(myTickets);
        }
        public async Task<IActionResult> CreateNewVenue(CreateVenueDTO createVenueDTO)
        {
            var x = await _generalDAO.VytvorProstoryPROCEDURE(createVenueDTO);
            return RedirectToAction("UserMenu", "Home");
        }
        public async Task<IActionResult> CreateNewTickets(CreateTicketsDTO tickets)
        {
            var x = await _generalDAO.GenerujVstupenkyPROCEDURE(tickets);

            return RedirectToAction("UserMenu", "Home");
        }

    }
}