﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SocialLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IdToken { get; set; }
    }
}
