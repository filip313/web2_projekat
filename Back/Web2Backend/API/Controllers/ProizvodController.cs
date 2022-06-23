using AutoMapper;
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
    [Route("api/proizvod")]
    public class ProizvodController : Controller
    {
        private IProizvodService _proizvodService;

        public ProizvodController(IProizvodService service)
        {
            _proizvodService = service;
        }

        [HttpPost]
        [Route("proizvod/dodaj")]
        [Authorize(Roles = "Admin")]
        public IActionResult DodajProizvod([FromBody] ProizvodDto proizvod)
        {
            try
            {
                var noviProizvod = _proizvodService.DodajProizvod(proizvod);

                return Created("proizvod", noviProizvod);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public IActionResult GetProizvode()
        {
            try
            {
                return Ok(_proizvodService.GetProizvode());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
