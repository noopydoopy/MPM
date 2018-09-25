﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Model;
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
        public IActionResult Authenticate([FromBody]AuthenModel authen)
        {
            if (authen == null)
            {
                return BadRequest("parameter is null");
            }

            UserModel userModel = new UserModel();
            if (authen.GrantType == "password")
            {
                userModel = userRepository.Authenticate(authen.Email, authen.Password);
                if (userModel == null){
                    return BadRequest(new { message = "Invalid Username or Password" });
                }
            }
            else if (authen.GrantType == "refresh_token")
            {
                userModel = userRepository.RefreshToken(authen.RefreshToken);
                if (userModel == null)
                {
                    return BadRequest(new { message = "Invalid Refresh Token" });
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok(userModel);
        }

        [HttpGet("GetUserNotInProjectId/{projectId}")]
        public List<UserProjectManageModel> GetUserNotInProjectId([FromRoute] int projectId)
        {
            var users = userRepository.GetUserNotinProject(projectId);
            return users;
        }

        [HttpGet("GetUserInProjectId/{projectId}")]
        public List<UserProjectManageModel> GetUserInProjectId([FromRoute] int projectId)
        {
            var users = userRepository.GetUserInProject(projectId);
            return users;
        }
    }
}