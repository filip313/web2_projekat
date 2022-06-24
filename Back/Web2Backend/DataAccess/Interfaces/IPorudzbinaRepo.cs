using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IPorudzbinaRepo
    {
        List<Porudzbina> GetPorudzbine();
        Porudzbina AddNew(Porudzbina newPorudzbina);
        List<Porudzbina> GetUserPorudzbine(int userId);
    }
}
