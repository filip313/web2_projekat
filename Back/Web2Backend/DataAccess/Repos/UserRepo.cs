using DataLayer.Interfaces;
using DataLayer.Models;
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

        public User AddUser(User newUser)
        {
            _db.Users.Add(newUser);
            _db.SaveChanges();

            return newUser;
        }

        public User GetUserByUsername(string username)
        {
            return _db.Users.Where(x => x.Username == username).FirstOrDefault();
        }
    }
}
