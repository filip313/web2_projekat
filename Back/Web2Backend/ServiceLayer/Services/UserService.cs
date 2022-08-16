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
using System.IO;
using System.Linq;
using System.Net.Http;
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
        private readonly IEmailSender _emailSender;

        public UserService(IMapper mapper, IUserRepo userRepo, IConfiguration config, IEmailSender sender)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _secretKey = config.GetSection("SecretKey");
            _emailSender = sender;
        }

        public TokenDto Login(UserLoginDto user)
        {
            User dbUser = _userRepo.GetUserByUsername(user.Username);

            if (null == dbUser)
            {
                throw new Exception("Korisnik sa ovim korisnickim imenom ne postoji!");
            }

            if (BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                List<Claim> claims = new List<Claim>();

                if (dbUser.UserType == UserType.Admin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, UserType.Admin.ToString()));
                }
                else if (dbUser.UserType == UserType.Dostavljac)
                {
                    claims.Add(new Claim(ClaimTypes.Role, UserType.Dostavljac.ToString()));
                }
                else if (dbUser.UserType == UserType.Potrosac)
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

                if (user.UserType == UserType.Admin)
                {
                    //throw new Exception("Los zahtev!");
                }
                else if (_userRepo.GetUserByUsername(user.Username) != null)
                {
                    throw new Exception("Korisnik sa tim korisnickim imenom vec postoji!");
                }

                string putanja = "";
                if (newUser.File != null && newUser.File.Length > 0)
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

            if (user.Username.Equals(string.Empty)
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
            if (!dbUser.Slika.Equals(string.Empty))
            {
                byte[] imageByte = File.ReadAllBytes(dbUser.Slika);
                dbUser.Slika = Convert.ToBase64String(imageByte);
            }

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

            if (dostavljac == null)
            {
                throw new Exception("Trazeni dostavljac ne postoji!");
            }
            else if (dostavljac.Verifikovan == info.Verifikacija || dostavljac.UserType != UserType.Dostavljac)
            {
                throw new Exception("Nije moguce izmeniti ovu verifikaciju!");
            }

            string trenutniStatus = dostavljac.Verifikovan ? "Verifikovan" : "Odbijen";
            string noviStatus = info.Verifikacija ? "Verifikovan" : "Odbijen";

            var message = new Message(new string[] { dostavljac.Email }, "Obavestenje o promeni statusa vaseg naloga.", $"Doslo je do promene statusa vaseg naloga.\n" +
                $"Novi statu je {noviStatus}.\nPrethodni status je {trenutniStatus}.");
            _emailSender.SendEmail(message);
            dostavljac.Verifikovan = info.Verifikacija;

            _userRepo.SaveChangedData(dostavljac);

            return _mapper.Map<UserDto>(dostavljac);
        }

        public UserDto IzmeniUsera(UserIzmenaDto izmena)
        {
            var dbUser = _userRepo.GetUserById(izmena.Id);
            if (dbUser != _userRepo.GetUserByUsername(izmena.Username))
            {
                throw new Exception("Los zahtev");
            }

            if (BCrypt.Net.BCrypt.Verify(izmena.StariPassword, dbUser.Password))
            {
                if (izmena.NoviPassword != null && !izmena.NoviPassword.Equals(string.Empty))
                {
                    dbUser.Password = BCrypt.Net.BCrypt.HashPassword(izmena.NoviPassword);
                }

                if (izmena.Ime != null)
                    dbUser.Ime = izmena.Ime.Equals(string.Empty) ? dbUser.Ime : izmena.Ime;
                if (izmena.Prezime != null)
                    dbUser.Prezime = izmena.Prezime.Equals(string.Empty) || izmena.Prezime == null ? dbUser.Prezime : izmena.Prezime;
                if (izmena.Email != null)
                    dbUser.Email = (izmena.Ime.Equals(string.Empty) && IsValidEmail(izmena.Email)) ? dbUser.Email : izmena.Email;
                if (izmena.DatumRodjenja != null)
                {
                    dbUser.DatumRodjenja = (DateTime)izmena.DatumRodjenja;
                }
                if (izmena.Adresa != null)
                    dbUser.Adresa = izmena.Adresa.Equals(string.Empty) || izmena.Adresa == null ? dbUser.Adresa : izmena.Adresa;
                if (izmena.File != null)
                {
                    string path = _userRepo.SaveImage(izmena.File, dbUser.Username);
                    dbUser.Slika = path;
                }
                _userRepo.SaveChangedData(dbUser);
                if (!dbUser.Slika.Equals(string.Empty))
                {
                    byte[] imageByte = File.ReadAllBytes(dbUser.Slika);
                    dbUser.Slika = Convert.ToBase64String(imageByte);
                }

                return _mapper.Map<UserDto>(dbUser);
            }

            throw new Exception("Greska prilikom autentifikacije!");
        }

        public TokenDto SocialLogin(SocialLoginDto data)
        {
            if (PotvrdiToken(data.IdToken))
            {
                var dbUser = _userRepo.GetUserByEmail(data.Email);

                if(dbUser == null)
                {
                    dbUser = _userRepo.AddUser(new User() { Email = data.Email, Adresa = "", DatumRodjenja = DateTime.Now,
                            Ime = data.FirstName, Slika = "", Password = "", Prezime = data.LastName, Username=data.Email, UserType = UserType.Potrosac
                    });
                }

               List<Claim> claims = new List<Claim>();


               claims.Add(new Claim(ClaimTypes.Role, UserType.Potrosac.ToString()));
               claims.Add(new Claim("id", dbUser.Id.ToString()));

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5002",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signingCredentials);

                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new TokenDto() { Token = token};
            }

            throw new Exception();

        }

        private bool PotvrdiToken(string token)
        {
            var httpClient = new HttpClient();
            var requestUri = new Uri(string.Format("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}", token));

            HttpResponseMessage response;
            try
            {
                response = httpClient.GetAsync(requestUri).Result;
            }
            catch (Exception e)
            {
                return false;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }

            var content = response.Content.ReadAsStringAsync().Result;

            return true;
        }
    }

}
