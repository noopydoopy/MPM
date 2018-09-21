using MPM.Databases.Models;
using MPM.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace MPM.Repository.Interfaces
{
    public interface IProjectRepository
    {
        List<Project> GetAllProjects();
        Project GetProjectByID(int id);
        void AddProject(Project session);
        void UpdateProject(Project session);
        void DeleteProject(int id);
        ProjectManageModel GetProjectManageByProjectId(int projectId);

    }
}
