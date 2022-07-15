using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class PorudzbinaDto
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public decimal Cena { get; set; }
        public decimal CenaDostave { get; set; }
        public TimeSpan TrajanjeDostave { get; set; }
        public DateTime VremePrihvata { get; set; }
        public string Status { get; set; }
        public List<PorudzbinaProizvodDto> Proizvodi { get; set; }
        public UserDto Narucilac { get; set; }
        public UserDto Dostavljac { get; set; }
    }
}
