using Back_end.DTOS.Home;
using Back_end.DTOS.Validation.HomeValidation;
using Back_end.Models;
using Back_end.Services.HomeService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        public readonly IHomeService _homeService;
        public HomeController(UserManager<ApplicationUser> manager, IHomeService homeService)
        {
            _homeService = homeService;
        }
        #region Create
        [HttpPost]
        public async Task<ActionResult> CreateHome([FromBody] HomeCreateDto homeCreateDto)
        {
            HomeCreateValidation validationRules = new HomeCreateValidation();
            var validatorResults = await validationRules.ValidateAsync(homeCreateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var home = await _homeService.AddHome(homeCreateDto);
            if (!home.NotFoundMassage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(home.NotFoundMassage);
                return NotFound(new { errors = error });
            }
            return Ok(home);
        }
        #endregion

        #region Update 

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateHome(HomeUpdateDto homeUpdateDto, int Id)
        {
            HomeUpdateValidation validationRules = new HomeUpdateValidation();
            var validatorResults = await validationRules.ValidateAsync(homeUpdateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var home = await _homeService.UpdateHome(homeUpdateDto, Id);
            if (!home.NotFoundMassage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(home.NotFoundMassage);
                return NotFound(new { errors = error });
            }
            if (!home.Massage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(home.Massage);
                return BadRequest(new { errors = error });
            }
            return Ok(home);

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
                return NotFound(new { errors = error });
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
                return NotFound(new { errors = error });
            }

            return Ok(home);
        }
        #endregion

        #region ViewAllHome

        [HttpGet("All-Homes")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewAllHome()
        {
            var homes = await _homeService.ViewsHome();
            if (homes == null || homes.Count() == 0)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { errors = error });
            }
            return Ok(homes);
        }

        #endregion

        #region delete
        [HttpDelete("remove/{id:int}")]
        public async Task<ActionResult> RemoveHome(int Id)
        {
            string result = await _homeService.RemoveHome(Id);

            if (!result.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(result);
                return NotFound(new { errors = error });

            }

            return Ok(new { massage = "Home Is Deleted" });


        }

        #endregion


        [HttpGet]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHome()
        {
            var homes = await _homeService.ViewsHomeNotDelete();
            if (homes == null || homes.Count() == 0)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { errors = error });
            }
            return Ok(homes);
        }


        [HttpGet("Deleted-Homes")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHomeDelete()
        {
            var homes = await _homeService.ViewsHomeDelete();
            if (homes == null || homes.Count() == 0)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { errors = error });
            }
            return Ok(homes);
        }


        [HttpGet("GetIds")]
        public async Task<ActionResult<List<int>>> GetIds()
        {
            var homes = await _homeService.GetIdsOfHomes();
            if (homes == null || (homes.Count == 0))
            {
                List<string> error = new List<string>();
                error.Add("Not Found Homes");
                return NotFound(new { errors = error });
            }
            return Ok(new { Ids = homes });

        }

        [HttpGet("GetMl/{id}")]
        public async Task<ActionResult> get_data(int id)
        {
            var results = await _homeService.GetHomeEmailAndPhone(id);
            return Ok(results);

        }

    }
}
