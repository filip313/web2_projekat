using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SocialLoginDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
    }
}
