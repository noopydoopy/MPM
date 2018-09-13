using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class Project
    {
        public Project()
        {
            Task = new HashSet<Task>();
            UserProject = new HashSet<UserProject>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Task> Task { get; set; }
        public ICollection<UserProject> UserProject { get; set; }
    }
}
