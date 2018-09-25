using System;

namespace MPM.Model
{
    public class TokenModel
    {
        public string Key { get; set; }
        public int TokenExpire { get; set; }
        public int RefreshTokenExpire { get; set; }
    }
}
