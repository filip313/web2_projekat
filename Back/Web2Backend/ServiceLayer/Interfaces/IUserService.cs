using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        UserRegistrationDto Register(UserRegistrationDto newUser);
        TokenDto Login(UserLoginDto user);
        UserDto GetUser(int id);
        List<UserDto> GetDostavljace();
        UserDto Verifikuj(VerifikacijaDto info);
    }
}
