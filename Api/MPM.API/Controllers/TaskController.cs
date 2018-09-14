using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;

namespace MPM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;

        public TaskController(ITaskRepository context)
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
        public async System.Threading.Tasks.Task<IActionResult> AddTask([FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskRepository.AddTask(task);

            return CreatedAtAction("GetPriority", new { id = task.TaskId }, task);
        }

        //DELETE
        [Route("delete")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTask(int id)
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
        public async System.Threading.Tasks.Task<IActionResult> PutTask([FromBody]Task task)
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
