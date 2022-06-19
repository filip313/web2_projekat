using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{

    public enum StatusPorudzbine
    {
        CekaDostavu,
        DostavljaSe,
        Dostavljena
    };

    public enum StatusNaloga
    {
        NaCekanju,
        Odobren,
        Odbijen
    };

    public enum UserType
    {
        Admin,
        Potrosac,
        Dostavljac
    }
}
