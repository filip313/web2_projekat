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

        public PorudzbinaService(IPorudzbinaRepo repo, IMapper mapper)
        {
            _porudzbinaRepo = repo;
            _mapper = mapper;
        }
        public List<PorudzbinaDto> GetPorudzbine()
        {
            var data = _porudzbinaRepo.GetPorudzbine();

            return _mapper.Map<List<Porudzbina>, List<PorudzbinaDto>>(data);
        }
    }
}
