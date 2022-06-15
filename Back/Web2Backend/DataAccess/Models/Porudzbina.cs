﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public decimal Cena { get; set; }
        public TimeSpan TrajanjeDostave { get; set; }
        public StatusPorudzbine Status { get; set; }
        public List<PorudzbinaProizvod> Proizvodi { get; set; }
    }
}
