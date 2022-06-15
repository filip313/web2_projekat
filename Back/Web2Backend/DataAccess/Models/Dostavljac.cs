using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Dostavljac : UserBase
    {
        public int Id { get; set; }
        public bool Verifikovan { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
    }
}
