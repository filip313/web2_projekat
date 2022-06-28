using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
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

        public Porudzbina AddNew(Porudzbina newPorudzbina)
        {
            Porudzbina toBeAdded = new Porudzbina
            {
                Adresa = newPorudzbina.Adresa,
                Cena = newPorudzbina.Cena,
                Komentar = newPorudzbina.Komentar,
                UserId = newPorudzbina.UserId,
                Proizvodi = new List<PorudzbinaProizvod>()
            };
            foreach(var item in newPorudzbina.Proizvodi)
            {
                var proizvod = _db.Proizvodi.Find(item.Proizvod.Id);

                toBeAdded.Proizvodi.Add(new PorudzbinaProizvod()
                {
                    Proizvod = proizvod,
                    Porudzbina = toBeAdded,
                    Kolicina = item.Kolicina
                });
            }

            _db.Porudzbine.Add(toBeAdded);
            _db.SaveChanges();

            return newPorudzbina;
        }

        public Porudzbina GetPorudzbinaById(int id)
        {
            var dbPorudzbina = _db.Porudzbine.Include(x => x.Proizvodi)
                                             .ThenInclude(x => x.Proizvod)
                                             .FirstOrDefault();
            
            return dbPorudzbina;
        }

        public List<Porudzbina> GetPorudzbine()
        {
            return _db.Porudzbine.Include(x => x.Proizvodi).ThenInclude(x => x.Proizvod).Include(x => x.User).ToList();
        }

        public List<Porudzbina> GetUserPorudzbine(int userId)
        {
            var porudzbine = _db.Porudzbine.Where(x => x.UserId == userId).Include(x => x.Proizvodi).ThenInclude(x => x.Proizvod).Include(x => x.User).ToList();

            if(porudzbine == null || porudzbine.Count == 0)
            {
                throw new Exception("Los zahtev, porudzbina ne postoji za tog usera!");
            }

            return porudzbine;
        }

        public void SaveChangedData(Porudzbina porudzbina)
        {
            _db.Porudzbine.Update(porudzbina);
            _db.SaveChanges();
        }
    }
}
