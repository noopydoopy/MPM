using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class User
    {
        public User()
        {
            Task = new HashSet<Task>();
            UserProject = new HashSet<UserProject>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Task> Task { get; set; }
        public ICollection<UserProject> UserProject { get; set; }
    }
}
