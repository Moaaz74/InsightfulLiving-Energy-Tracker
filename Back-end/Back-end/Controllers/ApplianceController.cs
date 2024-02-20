using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplianceController : ControllerBase
    {
        private readonly IApplianceDAO _applianceDAO;

        public ApplianceController(IApplianceDAO applianceDAO)
        {

            _applianceDAO = applianceDAO;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppliance()
        {
            var applianceDtos = new List<ApplianceDto>();
            var allAppliances = await _applianceDAO.getAppliance();
            if (!allAppliances.Any())
            {
                List<string> error = new List<string>();
                error.Add("There is no appliances consumption yet...");
                return NotFound(new { erroes = error });
            }

            ApplianceDto applianceDto;


            foreach (var appliance in allAppliances)
            {
                applianceDto = new ApplianceDto();
                applianceDto.Start = appliance.start;
                applianceDto.End = appliance.end;
                applianceDto.ApplianceConsumption = appliance.applianceconsumption;
                applianceDto.ApplianceId = appliance.applianceid;
                applianceDto.EnergyType = appliance.energytype;
                applianceDtos.Add(applianceDto);
            }
            return Ok(applianceDtos);
        }

    
}
}
