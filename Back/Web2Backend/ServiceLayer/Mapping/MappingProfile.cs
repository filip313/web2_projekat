using AutoMapper;
using DataLayer.Models;
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
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<PorudzbinaProizvod, PorudzbinaProizvodDto>().ReverseMap();
            CreateMap<Porudzbina, NovaPorudzbinaDto>()
                .ForMember(x => x.Proizvodi, act => act.MapFrom(src => src.Proizvodi))
                .ReverseMap();
            CreateMap<Porudzbina, PorudzbinaDto>()
                .ForMember(x => x.Proizvodi, act => act.MapFrom(src => src.Proizvodi))
                .ForMember(x => x.Narucilac, act => act.MapFrom(src => src.Narucialc))
                .ForMember(x => x.Dostavljac, act => act.MapFrom(src => src.Dostavljac))
                .ReverseMap();
            CreateMap<Proizvod, ProizvodDto>().ReverseMap();
        }
    }
}
