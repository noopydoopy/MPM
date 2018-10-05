using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPM.API.ModelViews
{
    public class SaveUserProjectModel
    {
        public int ProjectId { get; set; }

        public List<int> UserIdList { get; set; }

        public SaveUserProjectModel()
        {

        }
        public SaveUserProjectModel(int projectId,List<int> userIdList)
        {
            ProjectId = projectId;
            UserIdList = userIdList;
        }
    }
}
