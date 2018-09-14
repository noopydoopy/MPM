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
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityRepository priorityRepository;

        public PrioritiesController(IPriorityRepository context)
        {
            priorityRepository = context;
        }

        // GET: api/Priorities
        [HttpGet]
        public List<Priority> GetPriority()
        {
            return priorityRepository.GetAllPrioritys();
        }

        // GET: api/Priorities/5
        [HttpGet("{id}")]
        public IActionResult GetPriority([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var priority = priorityRepository.GetPriorityByID(id);

            if (priority == null)
            {
                return NotFound();
            }

            return Ok(priority);
        }

        // PUT: api/Priorities/5
        [HttpPut("{id}")]
        public IActionResult PutPriority([FromRoute] int id, [FromBody] Priority priority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priority.PriorityId)
            {
                return BadRequest();
            }

            priorityRepository.UpdatePriority(priority);

            return Ok();
        }

        // POST: api/Priorities
        [HttpPost]
        public IActionResult PostPriority([FromBody] Priority priority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            priorityRepository.AddPriority(priority);

            return CreatedAtAction("GetPriority", new { id = priority.PriorityId }, priority);
        }

        // DELETE: api/Priorities/5
        [HttpDelete("{id}")]
        public IActionResult DeletePriority([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var priority = priorityRepository.GetPriorityByID(id);
            if (priority == null)
            {
                return NotFound();
            }

            priorityRepository.DeletePriority(id);

            return Ok();
        }
    }
}