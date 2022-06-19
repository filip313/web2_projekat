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
    [Route("api/user")]
    [ApiController]
    public class UserControler : Controller
    {
        private readonly IUserService _userService;

        public UserControler(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRegistrationDto newUser)
        {
            try
            {
                var createdUser = _userService.Register(newUser);
                return Created("user/register", createdUser);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
