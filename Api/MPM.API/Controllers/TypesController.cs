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
    public class TypesController : ControllerBase
    {
        private readonly ITypeRepository repository;

        public TypesController(ITypeRepository context)
        {
            repository = context;
        }

        // GET: api/Types
        [HttpGet]
        public List<Databases.Models.Type> GetType()
        {
            return repository.GetAllTypes();
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public IActionResult GetType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @type = repository.GetTypeByID(id);

            if (@type == null)
            {
                return NotFound();
            }

            return Ok(@type);
        }

        // PUT: api/Types/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType([FromRoute] int id, [FromBody] Databases.Models.Type @type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @type.TypeId)
            {
                return BadRequest();
            }

            repository.UpdateType(@type);

            if (!TypeExists(id))
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/Types
        [HttpPost]
        public async Task<IActionResult> PostType([FromBody] Databases.Models.Type @type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.AddType(@type);

            return CreatedAtAction("GetType", new { id = @type.TypeId }, @type);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var type = repository.GetTypeByID(id);
            if (type == null)
            {
                return NotFound();
            }

            repository.DeleteType(id);

            return Ok(type);
        }

        private bool TypeExists(int id)
        {
            return repository.GetTypeByID(id) == null ? false : true;
        }
    }
}