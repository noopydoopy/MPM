using MPM.Databases.Models;
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
    }
}
