using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    }
}
