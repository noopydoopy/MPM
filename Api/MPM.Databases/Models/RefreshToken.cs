using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class RefreshToken
    {
        public string UserId { get; set; }
        public string Rtoken { get; set; }
        public DateTime RefreshTokenExpire { get; set; }
        public string UserDetail { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
