using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;

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

        //GET All       
        [Route("getall")]
        public List<Task> GetAllTasks()
        {
            return taskRepository.GetAllTasks();
        }

        //GET BY ID
        [Route("getbyid")]
        public Task GetTaskByID(int id)
        {
            return taskRepository.GetTaskByID(id);
        }

        //INSERT
        [Route("insert")]
        public IActionResult AddTask([FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskRepository.AddTask(task);

            return CreatedAtAction("GetTaskByID", new { id = task.PriorityId }, task);
        }

        //DELETE
        [Route("delete")]
        public IActionResult DeleteTask(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskRepository.DeleteTask(id);

            return Ok();
        }

        //UPDATE
        [Route("update")]
        public IActionResult PutTask([FromBody]Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskRepository.UpdateTask(task);

            return Ok();
        }
    }
}
