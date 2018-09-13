using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class Type
    {
        public Type()
        {
            Task = new HashSet<Task>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Task> Task { get; set; }
    }
}
