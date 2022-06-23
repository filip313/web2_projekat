using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IPorudzbinaService
    {
        List<PorudzbinaDto> GetPorudzbine();
    }
}
