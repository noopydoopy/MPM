using MPM.Databases.Models;
using System;

namespace MPM.API.ModelViews
{
    public class TaskModelView
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public string PriorityColor { get; set; }
        public string ProjectName { get; set; }
        public string Type { get; set; }
        public UserModelViews AssignTo { get; set; }
        public bool IsActive { get; set; }
        
        public TaskModelView(Task task)
        {
            TaskId = task.TaskId;
            Title = task.Title;
            Description = task.Description;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
            IsActive = task.IsActive;

            if (task.Priority != null)
            {
                Priority = task.Priority.PriorityNumber;
                PriorityColor = task.Priority.Color;
            }

            if (task.Project != null)
            {
                ProjectName = task.Project.Name;
            }

            if (task.Type != null)
            {
                Type = task.Type.Name;
            }

            if (task.AssignToNavigation != null)
            {
                AssignTo = new UserModelViews(task.AssignToNavigation);
            }
        }
    }
}
