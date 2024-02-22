using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries.ApplianceDtos;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

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
                return NotFound(new { errors = error });
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

        [HttpGet("{applianceid}")]
        public async Task<IActionResult> GetApplianceStartDates(int applianceid, [FromBody] object energyType)
        {
            JsonElement jsonObject = JsonSerializer.Deserialize<JsonElement>(energyType.ToString());

            // Access the "energyType" property value
            string energytype = jsonObject.GetProperty("energyType").GetString();

            if (energytype == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No EnergyType is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            var appliancestartdates = new List<String>();
            var allApplianceStartDates = await _applianceDAO.getApplianceStartDates(energytype, applianceid);
            if (allApplianceStartDates.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Appliances either StartDates are found !!...");
                return NotFound(new { errors = error });
            }
            HashSet<String> uniqueValues = new HashSet<String>();

            foreach (var appliancestartdate in allApplianceStartDates)
            {
                if (!uniqueValues.Contains(appliancestartdate))
                {
                    appliancestartdates.Add(appliancestartdate);
                    uniqueValues.Add(appliancestartdate);
                }
            }
            return Ok(appliancestartdates);
        }



        [HttpGet("EndDates/{applianceid}")]
        public async Task<IActionResult> GetApplianceEndDates(int applianceid, [FromBody] ApplianceS_DateDto s_DateDto)
        {
           
            if (s_DateDto.energyType == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No EnergyType is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            else if (s_DateDto.startDate == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No StartDate is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            var applianceenddates = new List<String>();
            var allApplianceEndDates = await _applianceDAO.getApplianceEndDates(s_DateDto, applianceid);
            if (allApplianceEndDates.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Appliances either EndDates are found !!...");
                return NotFound(new { errors = error });
            }
            HashSet<String> uniqueValues = new HashSet<String>();

            foreach (var applianceenddate in allApplianceEndDates)
            {
                if (!uniqueValues.Contains(applianceenddate))
                {
                    applianceenddates.Add(applianceenddate);
                    uniqueValues.Add(applianceenddate);

                }
            }
            return Ok(applianceenddates);
        }

        [HttpGet("data/{applianceid}")]
        public async Task<IActionResult> GetApplianceconsumption(int applianceid, [FromBody] ApplianceDatesDto datesDto)
        {
           

            if (datesDto.energyType == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No EnergyType is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            else if (datesDto.startDate == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No StartDate is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            else if (datesDto.endDate == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No EndDate is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            var appliancedata = new List<Double>();
            var allApplianceconsumptionvals = await _applianceDAO.getApplianceconsumption(datesDto, applianceid);
            if (allApplianceconsumptionvals.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Appliance Consumptions are found !!...");
                return NotFound(new { errors = error });
            }


            foreach (var value in allApplianceconsumptionvals)
            {
                appliancedata.Add(value);
            }
            return Ok(appliancedata);
        }

    }
}
