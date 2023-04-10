using Microsoft.AspNetCore.Mvc;
using ListkovacBL.DAO;
using ListkovacDTO;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Listkovac2Auth.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Listkovac2Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClanekController : ControllerBase
    {
        private readonly IGeneralDAO _generalDAO;
        private int LoggedUserId
        {
            get
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Properties.Any(x => x.Value == JwtRegisteredClaimNames.Sub));

                return int.Parse(idClaim!.Value);
            }
        }
        public ClanekController(IGeneralDAO generalDAO)
        {
            _generalDAO = generalDAO;
        }

        // GET api/<ClanekController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var clanek = await _generalDAO.GetClanekById(id);

            return Ok(clanek);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var clanky = await _generalDAO.GetClankyByUserId(LoggedUserId);

            return Ok(clanky);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> CreateClanek(CreateClanekRequest createClanekRequest)
        {
            ClanekDTO clanek = new ClanekDTO();
            clanek.AutorId = LoggedUserId;
            clanek.Name = createClanekRequest.name;
            clanek.Date = DateTime.Now;
            clanek.Text = createClanekRequest.text;
            await _generalDAO.CreateNewClanekAsync(clanek);
            return Ok();
        }

        // POST api/<BlogController>
        [HttpPost("komentar")]
        [Authorize]
        public async Task<IActionResult> CreateKomentar(CreateKomentarRequest createKomentarRequest)
        {

            KomentarDTO komentarDTO = new KomentarDTO();
            komentarDTO.Text = createKomentarRequest.Text;
            komentarDTO.UserId = LoggedUserId;
            komentarDTO.Time = DateTime.Now;
            komentarDTO.ClanekId = createKomentarRequest.ClanekId;
            await _generalDAO.CreateNewComentAsync(komentarDTO);
            return Ok();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateClanek(UpdateClanekRequest updateClanek)
        {
            ClanekDTO clanek = await _generalDAO.GetClanekById(updateClanek.Id);
            if(clanek == null )
            {
                return NotFound();
            }
            if(clanek.AutorId != LoggedUserId)
            {
                return NotFound();
            }
            clanek.Name = updateClanek.Name;
            clanek.Text = updateClanek.Text;

            await _generalDAO.EditClanekById(clanek);
            return Ok();
        }


    }
}
