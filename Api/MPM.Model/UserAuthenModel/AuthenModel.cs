using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Model
{
    public class AuthenModel
    {
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
