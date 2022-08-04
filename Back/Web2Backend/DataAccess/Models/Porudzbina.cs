using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public decimal Cena { get; set; }
        public decimal CenaDostave{ get; set; }
        public TimeSpan TrajanjeDostave { get; set; }
        public DateTime VremePrihvata{ get; set; }
        public StatusPorudzbine Status { get; set; }
        public List<PorudzbinaProizvod> Proizvodi { get; set; }
        public int NarucilacId { get; set; }
        public User Narucialc { get; set; }
        public int DostavljacId { get; set; }
        public User Dostavljac { get; set; }
        public string PayPalId { get; set; }
        public string PayPalStatus { get; set; }
        public NacinPlacanja NacinPlacanja { get; set; }
    }
}
