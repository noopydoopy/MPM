using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MPM.API.ModelViews
{
    public class UserLinkProjectModel
    {
        public int UserId { get; set; }
        public List<int> ProjectIdList { get; set; }

        public UserLinkProjectModel()
        {

        }
        public UserLinkProjectModel(int userId, List<UserProject> userProjects)
        {
            UserId = userId;
            if (userProjects != null && userProjects.Count > 0)
            {
                ProjectIdList = userProjects.Select(s => s.ProjectId).ToList();
            }
            else
                ProjectIdList = new List<int>();
        }
    }
}
