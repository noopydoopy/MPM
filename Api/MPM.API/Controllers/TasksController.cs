using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MPM.API.ModelViews;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MPM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;

        public TasksController(ITaskRepository context)
        {
            taskRepository = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public List<Task> GetTask()
        {
            return taskRepository.GetAllTasks();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public IActionResult GetTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = taskRepository.GetTaskByID(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult PutTask([FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskRepository.UpdateTask(task);

            return Ok();
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult PostTask([FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskRepository.AddTask(task);

            return CreatedAtAction("GetTaskByID", new { id = task.PriorityId }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = taskRepository.GetTaskByID(id);
            if (task == null)
            {
                return NotFound();
            }

            taskRepository.DeleteTask(id);

            return Ok();
        }

        //Get by ProjectID
        [Route("GetByProjectID/{id}")]
        public List<TaskModelView> GetTasksByProjectID([FromRoute] int id)
        {
            var tasks = taskRepository.GetAllTasksByProjectID(id);

            List<TaskModelView> taskList = new List<TaskModelView>();
            foreach (var task in tasks)
            {
                taskList.Add(new TaskModelView(task));
            }

            return taskList;
        }
    }
}