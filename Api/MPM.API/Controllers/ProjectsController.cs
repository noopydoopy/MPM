using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;

namespace MPM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsController(IProjectRepository repository)
        {
            projectRepository = repository;
        }

        // GET: api/Projects
        [HttpGet]
        public List<Project> GetProject()
        {
            return projectRepository.GetAllProjects();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public IActionResult GetProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = projectRepository.GetProjectByID(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            projectRepository.UpdateProject(project);
     
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            projectRepository.AddProject(project);


            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = projectRepository.GetProjectByID(id);
            if (project == null)
            {
                return NotFound();
            }

            projectRepository.DeleteProject(id);
           
            return Ok(project);
        }

        private bool ProjectExists(int id)
        {
            return projectRepository.GetProjectByID(id) == null?false:true;
        }
    }
}