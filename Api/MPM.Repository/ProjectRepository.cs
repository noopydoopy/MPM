using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Model;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPM.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly mpmContext _context;

        public ProjectRepository(mpmContext context)
        {
            _context = context;
        }

        void IProjectRepository.AddProject(Project session)
        {
            try
            {
                _context.Project.Add(session);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IProjectRepository.DeleteProject(int id)
        {
            try
            {
                Project removeProject = GetProjectByid(id);
                _context.Project.Remove(removeProject);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<Project> IProjectRepository.GetAllProjects()
        {
            List<Project> project = _context.Project.OrderBy(a => a.Name).ToList();
            return project;
        }

        Project IProjectRepository.GetProjectByID(int id)
        {
            return GetProjectByid(id);
        }

        void IProjectRepository.UpdateProject(Project session)
        {
            try
            {
                Project updateProject = GetProjectByid(session.ProjectId);
                if (updateProject != null)
                {
                    updateProject.Name = session.Name;
                    updateProject.IsActive = session.IsActive;
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("No Project id {0} data in database", session.ProjectId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Project GetProjectByid(int id)
        {
            var project = _context.Project.Where(p => p.ProjectId == id); ;
            if (project.Count() > 0)
                return project.First();
            else
                return null;
        }

        public ProjectManageModel GetProjectManageByProjectId(int projectId)
        {
            var project = _context.Project.Include(pro=>pro.UserProject)
                .Include(pro=>pro.Task)
                .Where(p => p.ProjectId == projectId)
                .FirstOrDefault();

            ProjectManageModel projectResult = new ProjectManageModel();
            if(project != null)
            {
                projectResult.ProjectId = project.ProjectId;
                projectResult.ProjectName = project.Name;
                projectResult.ProjectIsActive = project.IsActive;               
                projectResult.UserCount = project.UserProject.Count();
                projectResult.TaskCount = project.Task.Count();
                projectResult.TaskActiveCount = project.Task.Where(t => t.IsActive).Count();
            }

            return projectResult;
        }
    }
}
