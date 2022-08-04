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
        NovaPorudzbinaDto AddNew(NovaPorudzbinaDto novaPorudzbina);
        List<PorudzbinaDto> GetUserPorudzbine(int userId);

        PorudzbinaDto Prihvati(PrihvatiPorudzbinuDto prihvatDto);
        List<PorudzbinaDto> GetNove();
        PorudzbinaDto ZavrsiPorudzbinu(int id);
        bool Test(NovaPorudzbinaDto testPorudzbina);
    }
}
