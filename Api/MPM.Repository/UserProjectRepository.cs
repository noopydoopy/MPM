using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPM.Repository
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly mpmContext _context;

        public UserProjectRepository(mpmContext context)
        {
            _context = context;
        }

        public void AddUserProject(UserProject userProject)
        {
            try
            {
                _context.UserProject.Add(userProject);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteUserProject(int userProjectId)
        {
            try
            {
                var currentUserProject = _context.UserProject.Find(userProjectId);
                _context.UserProject.Remove(currentUserProject);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<UserProject> GetAllUserProjects()
        {
            return _context.UserProject.ToList();
        }

        public UserProject GetUserProjectById(int userProjectId)
        {
            return _context.UserProject.Find(userProjectId);
        }

        public List<UserProject> GetUserProjectByProjectId(int projectId)
        {
            return _context.UserProject.Where(up => up.ProjectId == projectId).ToList();
        }

        public List<UserProject> GetUserProjectByUserId(int userId)
        {
            return _context.UserProject.Where(up => up.UserId == userId).ToList();
        }
    
        public void UpdateUserProject(UserProject userProject)
        {
            try
            {
                _context.Entry(userProject).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void AddUserToUserProject(int projectId,List<int> userList )
        {
            try
            {
                List<UserProject> oldUserprojects = _context.UserProject.Where(up => up.ProjectId == projectId).ToList();
                List<UserProject> deleteUserProject = new List<UserProject>();
                List<UserProject> addUserProject = new List<UserProject>();
                List<int> userAdds = new List<int>();
                deleteUserProject = oldUserprojects.Where(up => !userList.Contains(up.UserId)).ToList();
                userAdds = userList.Where(u => !oldUserprojects.Any(up => up.UserId == u)).ToList();

                if(userAdds.Count >0)
                {
                    foreach (var userId in userAdds)
                    {
                        UserProject UserProject = new UserProject();
                        UserProject.ProjectId = projectId;
                        UserProject.UserId = userId;
                        addUserProject.Add(UserProject);
                    }
                }

                if(deleteUserProject != null && deleteUserProject.Count > 0)
                {
                    _context.UserProject.RemoveRange(deleteUserProject);
                    _context.SaveChanges();
                }
                if(addUserProject != null && addUserProject.Count > 0)
                {
                    _context.UserProject.AddRange(addUserProject);
                    _context.SaveChanges();
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
