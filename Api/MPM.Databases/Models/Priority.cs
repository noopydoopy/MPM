using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class Priority
    {
        public Priority()
        {
            Task = new HashSet<Task>();
        }

        public int PriorityId { get; set; }
        public int PriorityNumber { get; set; }
        public string Color { get; set; }

        public ICollection<Task> Task { get; set; }
    }
}
