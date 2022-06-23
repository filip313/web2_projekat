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
    public class ProizvodService : IProizvodService
    {
        private IProizvodRepo _proizvodRepo;
        private IMapper _mapper;

        public ProizvodService(IProizvodRepo repo, IMapper mapper)
        {
            _proizvodRepo = repo;
            _mapper = mapper;
        }

        public ProizvodDto DodajProizvod(ProizvodDto noviProizvod)
        {
            if (noviProizvod.Naziv.Equals(string.Empty) ||
                noviProizvod.Sastojci.Equals(string.Empty) ||
                noviProizvod.Cena <= 0)
            {
                throw new Exception("Losi podaci proizvoda!");
            }

            var dbProizvod = (Proizvod)_mapper.Map<ProizvodDto, Proizvod>(noviProizvod);
            try
            {
                dbProizvod = _proizvodRepo.Add(dbProizvod);
            }
            catch
            {
                throw new Exception("Proizvod sa ovim nazivom vec postoji!");
            }

            return (ProizvodDto)_mapper.Map<Proizvod, ProizvodDto>(dbProizvod);
        }

        public List<ProizvodDto> GetProizvode()
        {

            var proizvodi = _proizvodRepo.GetAll();

            return _mapper.Map<List<Proizvod>, List<ProizvodDto>>(proizvodi);

        }
    }
}
