using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Repository.Interfaces
{
    public interface IUserProjectRepository
    {
        List<UserProject> GetAllUserProjects();
        List<UserProject> GetUserProjectByUserId(int userId);
        List<UserProject> GetUserProjectByProjectId(int projectId);
        UserProject GetUserProjectById(int userProjectId);
        void AddUserProject(UserProject userProject);
        void UpdateUserProject(UserProject userProject);
        void DeleteUserProject(int userProjectId);
        void AddUserToUserProject(int projectId, List<int> userList);
    }
}
