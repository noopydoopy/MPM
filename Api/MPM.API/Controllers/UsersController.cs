using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;

namespace MPM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository context)
        {
            userRepository = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return userRepository.GetAllUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/code
        [HttpGet("code/{code}")]
        public IActionResult GetUserByCode([FromRoute] String code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userRepository.GetUserByCode(code);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            try
            {
                userRepository.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userRepository.AddUser(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            userRepository.DeleteUser(id);

            return Ok(user);
        }

        [HttpPost("authentication")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var tokenModel = userRepository.Authenticate(userParam.Email, userParam.Password);

            if (tokenModel == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(tokenModel);
        }
    }
}