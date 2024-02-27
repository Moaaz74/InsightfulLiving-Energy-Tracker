using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Back_end.DTOs;
using Back_end.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Back_end.DTOS.User;
using Back_end.Services;

namespace Back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IUserService _userService;

        private readonly IJwtService _jwtService;

        public UserController(UserManager<ApplicationUser> userManager , IJwtService jwtService , IUserService userService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _userService = userService;
        }

        #region AddUser
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User added successfully", UserId = user.Id });
            }
            return BadRequest(new { Message = "User addition failed", Errors = result.Errors });
            
        }
        #endregion

        #region UpdateUser

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] string UserId, [FromBody]UpdateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
                return NotFound("User not found");
            

            if (!user.IsPasswordChanged && model.Password != "Moa@@z_1234")
                model.isPasswordChanged = true;
            
            // Update user properties
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user , model.Password);
            

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User updated successfully" });
            }
            return BadRequest(new { Message = "User update failed", Errors = result.Errors });
            
        }

        #endregion

        #region GetUserById

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] string UserId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            if (user == null || user.IsDeleted)
            {
                return NotFound("User not found");
            }

            GetUserDto userDto = new GetUserDto()
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userDto);
        }

        [HttpGet("homes")]
        public IActionResult GetUserHomes([FromQuery] string UserId)
        {
            List<Home> homes = _userService.GetUserHomesById(UserId);
            return Ok(homes);

        }

        #endregion

        #region GetAllUsers

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            List<GetUserDto> usersDto = new List<GetUserDto>();

            foreach(ApplicationUser user in users)
            {
                usersDto.Add(new GetUserDto() { Username = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber });
            }

            return Ok(usersDto);
        }

        #endregion

        #region DeleteUser

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string UserId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            if (user == null || user.IsDeleted)
            {
                return NotFound("User not found");
            }
            user.IsDeleted = true;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if(result.Succeeded) 
                return Ok("User Deleted Successfully");
            return BadRequest(result);
        }

        #endregion

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user is null)
                return Unauthorized();
            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Unauthorized();

            AuthenticationResponseDTO token = _jwtService.CreateToken(user);
            return Ok(token);
        }

    }
}