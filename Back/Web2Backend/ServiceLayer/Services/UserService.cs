using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IConfigurationSection _secretKey;

        public UserService(IMapper mapper, IUserRepo userRepo, IConfiguration config)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _secretKey = config.GetSection("SecretKey");
        }

        public TokenDto Login (UserLoginDto user)
        {
            User dbUser = _userRepo.GetUserByUsername(user.Username);

            if(null == dbUser)
            {
                throw new Exception("Korisnik sa ovim korisnickim imenom ne postoji!");
            }

            if(BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                List<Claim> claims = new List<Claim>();

                if(dbUser.UserType == UserType.Admin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, UserType.Admin.ToString()));
                }
                else if(dbUser.UserType == UserType.Dostavljac)
                {
                    claims.Add(new Claim(ClaimTypes.Role, UserType.Dostavljac.ToString()));
                }
                else if(dbUser.UserType == UserType.Potrosac)
                {
                    claims.Add(new Claim(ClaimTypes.Role, UserType.Potrosac.ToString()));
                }

                claims.Add(new Claim("id", dbUser.Id.ToString()));

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5002",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signingCredentials);

                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new TokenDto() { Token = token };
            }
            else
            {
                throw new Exception("Pogresno uneta sifra!");
            }

        }

        public UserRegistrationDto Register(UserRegistrationDto newUser)
        {
            if (ValidateUserData(newUser))
            {
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                var user = (User)_mapper.Map(newUser, typeof(UserRegistrationDto), typeof(User));

                if(user.UserType == UserType.Admin)
                {
                    //throw new Exception("Los zahtev!");
                }
                else if(_userRepo.GetUserByUsername(user.Username) != null)
                {
                    throw new Exception("Korisnik sa tim korisnickim imenom vec postoji!");
                }

                string putanja = "";
                if(newUser.File.Length > 0)
                {
                    putanja = _userRepo.SaveImage(newUser.File, newUser.Username);
                }
                user.Slika = putanja;
                user = _userRepo.AddUser(user);
                return (UserRegistrationDto)_mapper.Map(user, typeof(User), typeof(UserRegistrationDto));
            }
            else
            {
                throw new Exception("Los zahtev, proveri ispravnost podataka!");
            }

            throw new Exception("Los zahtev!");
        }

        private bool ValidateUserData(UserRegistrationDto user)
        {
            bool ret = true;

            if(user.Username.Equals(string.Empty) 
                || user.Password.Equals(string.Empty) 
                || !IsValidEmail(user.Email)
                || user.Ime.Equals(string.Empty)
                || user.Prezime.Equals(string.Empty)
                || user.Adresa.Equals(string.Empty))
            {
                ret = false;
            }

            return ret;
        }

        private bool IsValidEmail(string email)
        {

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }

        }

        public UserDto GetUser(int id)
        {
            var dbUser = _userRepo.GetUserById(id);

            return (UserDto)_mapper.Map(dbUser, typeof(User), typeof(UserDto));
        }

        public List<UserDto> GetDostavljace()
        {
            var dostavljaci = _userRepo.GetDostavljace();

            return _mapper.Map<List<UserDto>>(dostavljaci);
        }

        public UserDto Verifikuj(VerifikacijaDto info)
        {
            var dostavljac = _userRepo.GetUserById(info.DostavljacId);

            if(dostavljac == null)
            {
                throw new Exception("Trazeni dostavljac ne postoji!");
            }
            else if(dostavljac.Verifikovan == info.Verifikacija || dostavljac.UserType != UserType.Dostavljac)
            {
                throw new Exception("Nije moguce izmeniti ovu verifikaciju!");
            }

            dostavljac.Verifikovan = info.Verifikacija;

            _userRepo.SaveChangedData(dostavljac);

            return _mapper.Map<UserDto>(dostavljac);
        }
    }
}
