using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Repository.Interfaces
{
    public interface ITaskRepository
    {
        List<Task> GetAllTasks();
        Task GetTaskByID(int id);
        List<Task> GetAllTasksByProjectID(int id);
        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int id);
    }
}