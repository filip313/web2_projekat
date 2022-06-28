using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class NovaPorudzbinaDto
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public decimal Cena { get; set; }
        public List<PorudzbinaProizvodDto> Proizvodi { get; set; }
        public int NarucilacId { get; set; }
    }
}
