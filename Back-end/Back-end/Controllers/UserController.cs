using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Back_end.DTOs;
using Back_end.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        #region AddUser
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
            var validationResult = AddUserValidation.Validator.Validate(model);
            if (validationResult != ValidationResult.Success)
            {
                return BadRequest(validationResult);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User added successfully", UserId = user.Id });
            }
            else
            {
                return BadRequest(new { Message = "User addition failed", Errors = result.Errors });
            }
        }
        #endregion

        #region UpdateUser

        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto model)
        {
            var validationResult = UpdateUserValidation.Validator.Validate(model);
            if (validationResult != ValidationResult.Success)
            {
                return BadRequest(validationResult);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update user properties
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = model.PasswordHash;
            

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User updated successfully" });
            }
            else
            {
                return BadRequest(new { Message = "User update failed", Errors = result.Errors });
            }
        }

        #endregion

        #region GetUserById

        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserById(GetUserByIdDto model)
        {
            var validationResult = GetUserByIdValidation.Validator.Validate(model);
            if (validationResult != ValidationResult.Success)
            {
                return BadRequest(validationResult);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        #endregion

        #region GetAllUsers

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync(); 

            return Ok(users);
        }

        #endregion

    }
}