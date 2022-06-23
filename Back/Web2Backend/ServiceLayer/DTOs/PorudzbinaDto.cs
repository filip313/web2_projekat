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
        public TimeSpan TrajanjeDostave { get; set; }
        public string Status { get; set; }
        //public List<PorudzbinaProizvod> Proizvodi { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
    }
}
