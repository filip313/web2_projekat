using AutoMapper;
using DataAccess;
using DataAccess.Models;
using DataLayer.Interfaces;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public UserService(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public UserRegistrationDto Register(UserRegistrationDto newUser)
        {
            if (ValidateUserData(newUser))
            {
                if (newUser.UserType == "Dostavljac")
                {
                    var dostavljac = (Dostavljac)_mapper.Map(newUser, typeof(UserRegistrationDto),
                        typeof(Dostavljac));
                    
                    var user = _userRepo.AddDostavljac(dostavljac);
                    
                    return (UserRegistrationDto)_mapper.Map(user, typeof(Dostavljac),
                        typeof(UserRegistrationDto));
                }
                else if (newUser.UserType == "Potrosac")
                {
                    var potrosac = (Potrosac)_mapper.Map(newUser, typeof(UserRegistrationDto),
                        typeof(Potrosac));

                    var user = _userRepo.AddPotrosac(potrosac);

                    return (UserRegistrationDto)_mapper.Map(user, typeof(Potrosac),
                        typeof(UserRegistrationDto));
                }
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
    }
}
