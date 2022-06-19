using AutoMapper;
using DataAccess.Models;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Admin, UserRegistrationDto>().ReverseMap();
            CreateMap<Dostavljac, UserRegistrationDto>().ReverseMap();
            CreateMap<Potrosac, UserRegistrationDto>().ReverseMap();
        }
    }
}
