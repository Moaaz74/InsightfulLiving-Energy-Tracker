using Azure.Core;
using Back_end.DTOS.Home;
using Back_end.DTOS.Validation.HomeValidation;
using Back_end.Models;
using Back_end.Services.HomeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> _manager;
        public readonly IHomeService _homeService;
        public HomeController(UserManager<ApplicationUser> manager, IHomeService homeService)
        {
            _manager = manager;
            _homeService = homeService;
        }
        #region Create
        [HttpPost]
        public async Task<ActionResult> CreateHome([FromBody] HomeCreateDto homeCreateDto)
        {
            HomeCreateValidation validationRules = new HomeCreateValidation(_manager);
            var validatorResults = await validationRules.ValidateAsync(homeCreateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var home = await _homeService.AddHome(homeCreateDto);
            return Ok(new { massage = "Home added ", home = home });
        }
        #endregion

        #region Update 

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateHome(HomeUpdateDto homeUpdateDto, int Id)
        {
            HomeUpdateValidation validationRules = new HomeUpdateValidation(_manager);
            var validatorResults = await validationRules.ValidateAsync(homeUpdateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var home = await _homeService.UpdateHome(homeUpdateDto, Id);
            if (!home.Massage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(home.Massage);
                return NotFound(new { errors = error });
            }
            return Ok(new { massage = "Home Is Updated ", home = home });

        }
        #endregion

        #region home-with-rooms-user 

        [HttpGet("home-with-rooms-user/{id}")]
        public async Task<IActionResult> GetHomeWithRooms(int id)
        {
            var homeWithRooms = await _homeService.GetHomeWithRooms(id);

            if (homeWithRooms == null)
            {
                List<string> error = new List<string>();
                error.Add("Home Is Not Exist");
                return NotFound(new { erroes = error });
            }

            return Ok(homeWithRooms);
        }
        #endregion

        #region ViewHome

        [HttpGet("{id}")]
        public async Task<ActionResult<HomeViewDto>> ViewHome(int id)
        {
            var home = await _homeService.ViewHome(id);
            if (home == null)
            {
                List<string> error = new List<string>();
                error.Add("Home Is Not Exist");
                return NotFound(new { erroes = error });
            }

            return Ok(home);
        }
        #endregion

        #region ViewAllHome

        [HttpGet("All-Homes")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewAllHome()
        {
            var homes = await _homeService.ViewsHome();
            if (homes == null)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { erroes = error });
            }
            return Ok(homes);
        }

        #endregion

        #region delete
        [HttpPut("remove/{id}")]
        public async Task<ActionResult> RemoveHome(int Id)
        {
            string result = await _homeService.RemoveHome(Id);

            if (!result.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(result);
                return NotFound(new { erroes = error });
               
            }

            return Ok(new { massage = "Home Is Deleted" });


        }

        #endregion


        [HttpGet]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHome()
        {
            var homes = await _homeService.ViewsHomeNotDelete();
            if (homes == null)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { erroes = error });
            }
            return Ok(homes);
        }


        [HttpGet("Deleted-Homes")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHomeDelete()
        {
            var homes = await _homeService.ViewsHomeDelete();
            if (homes == null)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { erroes = error });
            }
            return Ok(homes);
        }

    }
}
