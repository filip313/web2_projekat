using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos
{
    public class PorudzbinaRepo : IPorudzbinaRepo
    {
        private DataContext _db;

        public PorudzbinaRepo(DataContext db)
        {
            _db = db;
        }

        public List<Porudzbina> GetPorudzbine()
        {
            return _db.Porudzbine.ToList();
        }
    }
}
