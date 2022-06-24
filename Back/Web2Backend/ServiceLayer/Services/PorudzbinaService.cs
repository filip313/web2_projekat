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
    public class PorudzbinaService : IPorudzbinaService
    {
        private IPorudzbinaRepo _porudzbinaRepo;
        private IMapper _mapper;
        private IUserRepo _userRepo;
        private IProizvodRepo _proizvodRepo;

        public PorudzbinaService(IPorudzbinaRepo repo, IMapper mapper, IUserRepo userRepo, IProizvodRepo pRepo)
        {
            _porudzbinaRepo = repo;
            _mapper = mapper;
            _userRepo = userRepo;
            _proizvodRepo = pRepo;
        }

        public NovaPorudzbinaDto AddNew(NovaPorudzbinaDto novaPorudzbina)
        {
            if (IsPorudzbinaValid(novaPorudzbina))
            {
                var random = new Random((int)DateTime.Now.Ticks);
                TimeSpan vremeDostave = new TimeSpan(0, random.Next(15, 60),0);

                var dbPorudzbina = (Porudzbina)_mapper.Map<NovaPorudzbinaDto, Porudzbina>(novaPorudzbina);
                dbPorudzbina.TrajanjeDostave = vremeDostave;
                dbPorudzbina = _porudzbinaRepo.AddNew(dbPorudzbina);

                return (NovaPorudzbinaDto)_mapper.Map<Porudzbina, NovaPorudzbinaDto>(dbPorudzbina);
            }
            else
            {
                throw new Exception("Los zahtev!");
            }
        }

        public List<PorudzbinaDto> GetPorudzbine()
        {
            var data = _porudzbinaRepo.GetPorudzbine();

            return _mapper.Map<List<Porudzbina>, List<PorudzbinaDto>>(data);
        }

        public List<PorudzbinaDto> GetUserPorudzbine(int userId)
        {
            var dbPorudzbine = _porudzbinaRepo.GetUserPorudzbine(userId);

            return _mapper.Map<List<Porudzbina>, List<PorudzbinaDto>>(dbPorudzbine);
        }

        private bool IsPorudzbinaValid(NovaPorudzbinaDto porudzbina)
        {
            decimal ukCena = 0;
            foreach(var item in porudzbina.Proizvodi)
            {
                ukCena += (item.Kolicina * item.Proizvod.Cena);
            }

            bool userExists = _userRepo.DoesUserExist(porudzbina.UserId);

            if(!(porudzbina.Cena == ukCena && porudzbina.Proizvodi.Count != 0) ||
                porudzbina.Adresa.Equals(string.Empty) ||
                !userExists)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
