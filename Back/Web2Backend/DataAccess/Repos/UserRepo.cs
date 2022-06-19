using DataAccess;
using DataAccess.Models;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos
{
    public class UserRepo : IUserRepo
    {
        private DataContext _db;
        public UserRepo(DataContext db)
        {
            _db = db;
        }
        public Admin AddAdmin(Admin newAdmin)
        {
            _db.Admini.Add(newAdmin);
            _db.SaveChanges();

            return newAdmin;
        }

        public Dostavljac AddDostavljac(Dostavljac newDostavljac)
        {
            _db.Dostavljaci.Add(newDostavljac);
            _db.SaveChanges();

            return newDostavljac;
        }

        public Potrosac AddPotrosac(Potrosac newPotrosac)
        {
            _db.Potrosaci.Add(newPotrosac);
            _db.SaveChanges();

            return newPotrosac;
        }
    }
}
