using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUserRepo
    {
        Admin AddAdmin(Admin newAdmin);
        Potrosac AddPotrosac(Potrosac newPotrosac);
        Dostavljac AddDostavljac(Dostavljac newDostavljac);
    }
}
