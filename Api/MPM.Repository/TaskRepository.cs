using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPM.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly mpmContext _context;

        public TaskRepository(mpmContext context)
        {
            _context = context;
        }

        //GET All
        public List<Task> GetAllTasks()
        {
            return _context.Task.ToList();
        }

        //GET BY ID
        public Task GetTaskByID(int id)
        {
            return _context.Task.Find(id);
        }

        //INSERT
        public void AddTask(Task task)
        {
            try
            {
                _context.Task.Add(task);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Can't Insert Task");
            }
        }

        //DELETE
        public void DeleteTask(int id)
        {
            Task task = _context.Task.Find(id);
            if (task != null)
            {
                try
                {
                    _context.Task.Remove(task);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Can't Delete Task");
                }
            }
        }

        //UPDATE
        public void UpdateTask(Task task)
        {
            try
            {
                _context.Entry(task).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<Task> GetAllTasksByProjectID(int id)
        {
            try
            {
                var tasks = _context.Task
                    .Where(t => t.ProjectId == id)
                    .Include(t => t.Priority)
                    .Include(t => t.Project)
                    .Include(t => t.AssignToNavigation)
                    .Include(t => t.Type)
                    .ToList();
                return tasks;
            }
            catch (Exception)
            {
                throw new Exception("Can't find by ProjectID");
            }
        }
    }
}
