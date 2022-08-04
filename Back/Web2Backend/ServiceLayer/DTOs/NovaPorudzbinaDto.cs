using DataLayer.Models;
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
        public decimal CenaDostave { get; set; }
        public List<PorudzbinaProizvodDto> Proizvodi { get; set; }
        public int NarucilacId { get; set; }
        public string PayPalId { get; set; }
        public string PayPalStatus{ get; set; }
        public string NacinPlacanja { get; set; }
    }
}
