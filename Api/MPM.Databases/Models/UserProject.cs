using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class UserProject
    {
        public int UserProject1 { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }
}
