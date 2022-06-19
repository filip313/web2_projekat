using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Dostavljac : UserBase
    {
        public int Id { get; set; }
        public StatusNaloga StatusNaloga { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
    }
}
