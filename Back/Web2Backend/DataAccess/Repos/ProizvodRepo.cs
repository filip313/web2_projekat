using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos
{
    public class ProizvodRepo : IProizvodRepo
    {
        private DataContext _db;

        public ProizvodRepo(DataContext db)
        {
            _db = db;
        }

        public Proizvod Add(Proizvod newProizvod)
        {
            _db.Proizvodi.Add(newProizvod);
            _db.SaveChanges();

            return newProizvod;
        }

        public List<Proizvod> GetAll()
        {
            return _db.Proizvodi.ToList();
        }
    }
}
