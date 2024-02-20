using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries;
using Back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home_OverallController : ControllerBase
    {
        private readonly IHome_OverallDAO _home_overallDAO;
      
        public Home_OverallController(IHome_OverallDAO home_OverallDAO )
        {
        
        _home_overallDAO = home_OverallDAO;
        }

        [HttpGet]
        public async Task<IActionResult> GetHome_Overall()
        {
           var home_OverallDtos = new List<Home_OverallDto>();
            var allHomes = await _home_overallDAO.getHome();
            if (allHomes.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("There is no homes consumption yet...");
                return NotFound(new { erroes = error });
            }
            
            Home_OverallDto homeDto;
            
            
            foreach (var home in allHomes) { 
                homeDto = new Home_OverallDto();
                homeDto.Start = home.start;
                homeDto.End = home.end;
                homeDto.HomeConsumption = home.homeconsumption;
                homeDto.HomeId = home.homeid;
                homeDto.EnergyType = home.energytype;
                home_OverallDtos.Add(homeDto);
            }
            return Ok(home_OverallDtos);
        }
       
    }
}
