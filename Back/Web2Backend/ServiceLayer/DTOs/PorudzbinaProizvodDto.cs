using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class PorudzbinaProizvodDto
    {
        public int Id { get; set; }
        public int ProizvodId { get; set; }
        public ProizvodDto Proizvod { get; set; }
        public uint Kolicina { get; set; }
    }
}
