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
                NarucilacId = newPorudzbina.NarucilacId,
                DostavljacId = newPorudzbina.NarucilacId,
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
            return _db.Porudzbine.Include(x => x.Proizvodi).ThenInclude(x => x.Proizvod).Include(x => x.Narucialc).ToList();
        }

        public List<Porudzbina> GetUserPorudzbine(int userId)
        {
            List<Porudzbina> ret = null;
            var user = _db.Users.Find(userId);
            if(user.UserType == UserType.Potrosac)
            {
                ret = _db.Porudzbine.Where(x => x.NarucilacId == userId).Include(x => x.Proizvodi).ThenInclude(x => x.Proizvod).Include(x => x.Dostavljac).ToList();
            }
            else if(user.UserType == UserType.Dostavljac)
            {
                ret = _db.Porudzbine.Where(x => x.DostavljacId == userId).Include(x => x.Proizvodi).ThenInclude(x => x.Proizvod).Include(x => x.Narucialc).ToList();
            }

            if(ret == null)
            {
                throw new Exception("Los zahtev, porudzbina ne postoji za tog usera!");
            }

            return ret;
        }

        public void SaveChangedData(Porudzbina porudzbina)
        {
            _db.Porudzbine.Update(porudzbina);
            _db.SaveChanges();
        }
    }
}
