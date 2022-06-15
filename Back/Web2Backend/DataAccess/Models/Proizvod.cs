using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal Cena { get; set; }
        public string Sastojci { get; set; }
        public List<PorudzbinaProizvod> Porudzbine { get; set; }
    }
}
