using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        public User GetUserById(int id)
        {
            User ret;
            if ((ret = _db.Users.Include(x => x.Porudzbine).Include(x => x.Dostave).Where( x => x.Id == id).FirstOrDefault()) != null)
            {
                return ret;
            }
            else
            {
                throw new Exception("Doslo je do greske!");
            }
        }

        public User GetUserByUsername(string username)
        {
            return _db.Users.Where(x => x.Username == username).FirstOrDefault();
        }

        public bool DoesUserExist(int id)
        {
            var user = _db.Users.Find(id);

            return user != null;
        }

        public void SaveChangedData(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public List<User> GetDostavljace()
        {
            return _db.Users.Where(x => x.UserType == UserType.Dostavljac).ToList();
        }

        public string SaveImage(IFormFile slika, string username)
        {
            string path = Path.Combine( Path.GetDirectoryName(Environment.CurrentDirectory),"DataAccess","Images", username);
            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                slika.CopyTo(fileStream);
            }

            return path;
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
