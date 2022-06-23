using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IProizvodService
    {
        ProizvodDto DodajProizvod(ProizvodDto noviProizvod);
        List<ProizvodDto> GetProizvode();
    }
}
