using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPM.API.ModelViews;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;

namespace MPM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectsController : ControllerBase
    {
        private readonly IUserProjectRepository userProjectRepository;

        public UserProjectsController(IUserProjectRepository context)
        {
            userProjectRepository = context;
        }

        // GET: api/UserProjects
        [HttpGet]
        public List<UserProject> GetUserProject()
        {
            return userProjectRepository.GetAllUserProjects();
        }

        // GET: api/UserProjects/5
        [HttpGet("{id}")]
        public IActionResult GetUserProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProject = userProjectRepository.GetUserProjectById(id);

            if (userProject == null)
            {
                return NotFound();
            }

            return Ok(userProject);
        }
        [HttpGet]
        public List<UserProject> GetUserProjectByProjectId([FromRoute] int projectId)
        {
            var userProject = userProjectRepository.GetUserProjectByProjectId(projectId);
            return userProject;
        }
        [HttpGet("GetUserProjectByUserId/{userId}")]
        public UserLinkProjectModel GetUserProjectByUserId([FromRoute] int userId)
        {
            var userProject = userProjectRepository.GetUserProjectByUserId(userId);
            UserLinkProjectModel ulpModel = new UserLinkProjectModel(userId, userProject);
            return ulpModel;
        }
        // PUT: api/UserProjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProject([FromRoute] int id, [FromBody] UserProject userProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProject.UserProject1)
            {
                return BadRequest();
            }

            try
            {
                userProjectRepository.UpdateUserProject(userProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/UserProjects
        [HttpPost]
        public IActionResult PostUserProject([FromBody] UserProject userProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userProjectRepository.AddUserProject(userProject);

            return CreatedAtAction("GetUserProject", new { id = userProject.UserProject1 }, userProject);
        }

        // DELETE: api/UserProjects/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProject = userProjectRepository.GetUserProjectById(id);
            if (userProject == null)
            {
                return NotFound();
            }

            userProjectRepository.DeleteUserProject(id);

            return Ok(userProject);
        }

        [HttpPost("SaveUserProject")]
        public IActionResult SaveUserProject([FromBody] SaveUserProjectModel projectModel)
        {
            if(projectModel != null)
            {
                userProjectRepository.AddUserToUserProject(projectModel.ProjectId, projectModel.UserIdList);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}