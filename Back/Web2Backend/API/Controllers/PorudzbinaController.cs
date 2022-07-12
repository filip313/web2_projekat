using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class PorudzbinaController : Controller
    {
        private IPorudzbinaService _porudzbinaService;

        public PorudzbinaController(IPorudzbinaService service)
        {
            _porudzbinaService = service;
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult Porudzbine()
        {
            try
            {
                return Ok(_porudzbinaService.GetPorudzbine());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles ="Potrosac")]
        public IActionResult NovaPorudzbina([FromBody]NovaPorudzbinaDto porudzbina)
        {
            try
            {

                var novaPorudzbina = _porudzbinaService.AddNew(porudzbina);

                return Created("porudzbina", novaPorudzbina);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        [Authorize]
        public IActionResult NovaPorudzbina(int userId)
        {
            try
            {
                return Ok(_porudzbinaService.GetUserPorudzbine(userId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("prihvati")]
        [Authorize(Roles = "Dostavljac")]
        public IActionResult PrihvatiPorudzbinu([FromBody]PrihvatiPorudzbinuDto prihvatDto)
        {
            try
            {
                return Ok(_porudzbinaService.Prihvati(prihvatDto));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("nove")]
        [Authorize(Roles = "Dostavljac")]
        public IActionResult GetNovePorudzbine()
        {
            try
            {
                return Ok(_porudzbinaService.GetNove());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("zavrsi")]
        [Authorize]
        public IActionResult ZavrsiPorudzbinu([FromBody] int porudzbinaId)
        {
            try
            {
                return Ok(_porudzbinaService.ZavrsiPorudzbinu(porudzbinaId));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
