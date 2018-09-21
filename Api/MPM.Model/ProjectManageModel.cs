using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Model
{
    public class ProjectManageModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool ProjectIsActive { get; set; }
        public int TaskActiveCount { get; set; }
        public int TaskCount { get; set; }
        public int UserCount { get; set; }

        public ProjectManageModel()
        {

        }
    }
}
