using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurgerStoreAPI.Data;
using BurgerStoreAPI.Models;
using BurgerStoreAPI.BusinessLayer;

namespace BurgerStoreAPI.Controllers
{
  

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUserAsync(user);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<int>> PostUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || !IsValidPhoneNumber(user.MobileNumber))
            {
                return BadRequest("Invalid user data.");
            }

            var existingUser = await _userService.GetAllUsersAsync();
            if (existingUser.Any(u => u.MobileNumber == user.MobileNumber))
            {
                return Conflict("User with this mobile number already exists.");
            }

            var userId = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }
    }
}