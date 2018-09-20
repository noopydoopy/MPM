using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPM.API.ModelViews
{
    public class UserModelViews
    {
        public int UserID { get; set; }
        public string Name { get; set; }

        public UserModelViews(User user)
        {
            UserID = user.UserId;
            Name = user.Name;
        }
    }
}
