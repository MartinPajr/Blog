using Microsoft.AspNetCore.Mvc;
using ListkovacBL.DAO;
using ListkovacDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Listkovac2Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KomentarController : ControllerBase
    {
        private readonly IGeneralDAO _generalDAO;

        public KomentarController(IGeneralDAO generalDAO)
        {
            _generalDAO = generalDAO;
        }

        // GET api/<ClanekController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var komentare = await _generalDAO.GetKomentareKClanku(id);

            return Ok(komentare);
        }

        // POST api/<BlogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
