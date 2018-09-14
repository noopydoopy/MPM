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
            var taskDb = _context.Task.SingleOrDefault(c => c.TaskId == task.TaskId);
            if (taskDb != null)
            {
                try
                {
                    taskDb.Title = task.Title;
                    taskDb.Description = task.Description;
                    taskDb.StartDate = task.StartDate;
                    taskDb.EndDate = task.EndDate;
                    taskDb.PriorityId = task.PriorityId;
                    taskDb.ProjectId = task.ProjectId;
                    taskDb.TypeId = task.TypeId;
                    taskDb.AssignTo = task.AssignTo;
                    taskDb.IsActive = task.IsActive;
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Can't Update Task");
                }
            }
        }
    }
}
