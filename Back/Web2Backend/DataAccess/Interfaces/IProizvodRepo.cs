using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IProizvodRepo
    {
        Proizvod Add(Proizvod newProizvod);
        List<Proizvod> GetAll();
    }
}
