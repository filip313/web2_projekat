using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUserRepo
    {
        User AddUser(User newUser);
        User GetUserByUsername(string username);
        User GetUserById(int id);
        bool DoesUserExist(int id);
        void SaveChangedData(User user);
        List<User> GetDostavljace();
        string SaveImage(IFormFile slika, string username);
    }
}
