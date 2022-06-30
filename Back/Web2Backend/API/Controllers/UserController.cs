using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto dto)
        {
            try
            {
                return Ok(_userService.Login(dto));
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm] UserRegistrationDto newUser)
        {
            try
            {
                var createdUser = _userService.Register(newUser);
                return Created("api/user/register", createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUser(int id)
        {
            try
            {
                return Ok(_userService.GetUser(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("dostavljaci")]
        [Authorize(Roles = "Admin")]
        public IActionResult Dostavljaci()
        {
            try
            {
                return Ok(_userService.GetDostavljace());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("verifikuj")]
        [Authorize(Roles = "Admin")]
        public IActionResult Verifikacija([FromBody] VerifikacijaDto info)
        {
            try
            {
                return Ok(_userService.Verifikuj(info));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("izmeni")]
        [Authorize]
        public IActionResult IzmeniProfil([FromForm] UserIzmenaDto izmenjenUser)
        {
            try
            {
                return Ok(_userService.IzmeniUsera(izmenjenUser));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
