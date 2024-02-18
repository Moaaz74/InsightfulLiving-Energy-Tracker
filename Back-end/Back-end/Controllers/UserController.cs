using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Back_end.Models;
using Microsoft.AspNetCore.Identity;

namespace Back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Password = model.Password,
                Email = model.Email,
                Age = model.Age,
                Name = model.Name,
                Gender = model.Gender,
                IsActive = true,
                HomeId = model.HomeId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User registered successfully", UserId = user.Id });
        }

        // Other controller actions (login, get profile, etc.) can be added here
    }
}
