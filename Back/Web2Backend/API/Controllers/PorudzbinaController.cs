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
    [Route("api/porudzbina")]
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
        [Route("nova")]
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
        [Authorize(Roles = "Potrosac")]
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

    }
}
