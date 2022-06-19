using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public StatusNaloga StatusNaloga { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public UserType UserType { get; set; }
        public string Slika { get; set; }
    }
}
