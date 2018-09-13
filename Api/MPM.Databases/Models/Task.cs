using System;
using System.Collections.Generic;

namespace MPM.Databases.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PriorityId { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public int? AssignTo { get; set; }
        public bool IsActive { get; set; }

        public User AssignToNavigation { get; set; }
        public Priority Priority { get; set; }
        public Project Project { get; set; }
        public Type Type { get; set; }
    }
}
