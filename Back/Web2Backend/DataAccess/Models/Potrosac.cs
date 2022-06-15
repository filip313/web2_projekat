using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Potrosac : UserBase
    {
        public int Id { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
    }
}
