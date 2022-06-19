using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
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
                var user = (User)_mapper.Map(newUser, typeof(UserRegistrationDto), typeof(User));

                if(user.UserType == UserType.Admin)
                {
                    throw new Exception("Los zahtev!");
                }
                else if(_userRepo.GetUserByUsername(user.Username) != null)
                {
                    throw new Exception("Korisnik sa tim korisnickim imenom vec postoji!");
                }

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
    }
}
