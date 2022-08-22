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
                var dbPorudzbina = (Porudzbina)_mapper.Map<NovaPorudzbinaDto, Porudzbina>(novaPorudzbina);
                var porudzbine = _porudzbinaRepo.GetUserPorudzbine(novaPorudzbina.NarucilacId);

                foreach (var item in porudzbine)
                {
                    if(item.Status == StatusPorudzbine.DostavljaSe || item.Status == StatusPorudzbine.CekaDostavu)
                    {
                        throw new Exception("Nije moguce praviti novu dostavu dok se trenutna ne zavrsi!");
                    }
                }
                dbPorudzbina = _porudzbinaRepo.AddNew(dbPorudzbina);

                return (NovaPorudzbinaDto)_mapper.Map<Porudzbina, NovaPorudzbinaDto>(dbPorudzbina);
            }
            else
            {
                throw new Exception("Los zahtev!");
            }
        }

        public List<PorudzbinaDto> GetNove()
        {
            var porudzbine = _porudzbinaRepo.GetNove();

            return _mapper.Map<List<PorudzbinaDto>>(porudzbine);
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

        public PorudzbinaDto Prihvati(PrihvatiPorudzbinuDto prihvatDto)
        {
            var dostavljac = _userRepo.GetUserById(prihvatDto.DostavljacId);
            var porudzbina = _porudzbinaRepo.GetPorudzbinaById(prihvatDto.PorudzbinaId);
            if(null == dostavljac || null == porudzbina)
            {
                throw new Exception("Los zahtev!");
            }
            if(porudzbina.Status != StatusPorudzbine.CekaDostavu)
            {
                throw new Exception("Nije moguce preuzeti ovu porudzbinu zato sto je vec dostavljena ili se dostavlja!");
            }
            if (!dostavljac.Verifikovan)
            {
                throw new Exception("Samo verifkovani dostavljaci mogu da prihvataju porudzbine!");
            }

            foreach(var item in dostavljac.Dostave)
            {
                if(item.Status == StatusPorudzbine.DostavljaSe)
                {
                    throw new Exception($"Nije moguce preuzeti novu porudzbinu dok porudzbina {item.Id} " +
                        $"nije zavrsena.");
                }
            }
            int minuti = new Random().Next(15, 70);
            TimeSpan trajanjeDostave = new TimeSpan(0, 0, minuti);
            DateTime vremePrihvata = DateTime.Now;

            porudzbina.TrajanjeDostave = trajanjeDostave;
            porudzbina.VremePrihvata = vremePrihvata;
            porudzbina.Status = StatusPorudzbine.DostavljaSe;
            porudzbina.Dostavljac = dostavljac;
            porudzbina.DostavljacId = dostavljac.Id;

            dostavljac.Dostave.Add(porudzbina);

            _porudzbinaRepo.SaveChangedData(porudzbina);
            _userRepo.SaveChangedData(dostavljac);

            return _mapper.Map<PorudzbinaDto>(porudzbina);
        }

        public NovaPorudzbinaDto Test(NovaPorudzbinaDto testPorudzbina)
        {
            if (IsPorudzbinaValid(testPorudzbina))
            {
                var porudzbine = _porudzbinaRepo.GetUserPorudzbine(testPorudzbina.NarucilacId);

                foreach (var item in porudzbine)
                {
                    if (item.Status == StatusPorudzbine.DostavljaSe || item.Status == StatusPorudzbine.CekaDostavu)
                    {
                        throw new Exception("Nije moguce napraviti novu porudzbinu dok se trenutna ne zavrsi!");
                    }
                }

                return testPorudzbina;
            }

            throw new Exception("Los Zahtev!");
        }

        public PorudzbinaDto ZavrsiPorudzbinu(int id)
        {
            var porudzbina = _porudzbinaRepo.GetPorudzbinaById(id);

            if(porudzbina == null || porudzbina.Status != StatusPorudzbine.DostavljaSe)
            {
                throw new Exception("Nije moguce zavrsiti ovu porudzbinu!");
            }

            DateTime now = DateTime.Now;
            if(porudzbina.VremePrihvata.Add(porudzbina.TrajanjeDostave) > now)
            {
                throw new Exception("Nije moguce zavrsiti dostavu pre isteka dostave");
            }

            porudzbina.Status = StatusPorudzbine.Dostavljena;
            _porudzbinaRepo.SaveChangedData(porudzbina);

            return _mapper.Map<PorudzbinaDto>(porudzbina);
        }

        private bool IsPorudzbinaValid(NovaPorudzbinaDto porudzbina)
        {
            decimal ukCena = 0;
            foreach(var item in porudzbina.Proizvodi)
            {
                ukCena += (item.Kolicina * item.Proizvod.Cena);
            }

            bool userExists = _userRepo.DoesUserExist(porudzbina.NarucilacId);
            ukCena += porudzbina.CenaDostave;
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
