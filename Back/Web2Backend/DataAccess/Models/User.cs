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
        public List<Porudzbina> Porudzbine { get; set; }
        public List<Porudzbina> Dostave { get; set; } = new List<Porudzbina>();
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool Verifikovan { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public UserType UserType { get; set; }
        public string Slika { get; set; }
    }
}
