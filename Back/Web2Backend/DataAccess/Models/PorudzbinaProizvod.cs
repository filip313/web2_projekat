using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PorudzbinaProizvod
    {
        public int Id { get; set; }
        public int PorudzbinaId { get; set; }
        public Porudzbina Porudzbina { get; set; }
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
        public uint Kolicina { get; set; }
    }
}
