﻿using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home_OverallController : ControllerBase
    {
        private readonly IHome_OverallDAO _home_overallDAO;

        public Home_OverallController(IHome_OverallDAO home_OverallDAO)
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
                return NotFound(new { errors = error });
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

        [HttpGet("{homeid}")]
        public async Task<IActionResult> GetHomeStartDates( int homeid,[FromBody] object energyType)            
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
            var homestartdates = new List<String>();
            var allHomeStartDates = await _home_overallDAO.getHomeStartDates(energytype,homeid);
            if (allHomeStartDates.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Homes either StartDates are found !!...");
                return NotFound(new { errors = error });
            }
            HashSet<String> uniqueValues = new HashSet<String>();

            foreach (var homestartdate in allHomeStartDates)
            {
                if (!uniqueValues.Contains(homestartdate))
                {
                    homestartdates.Add(homestartdate);
                    uniqueValues.Add(homestartdate);
                }
            }
            return Ok(homestartdates);
        }



        [HttpGet("EndDates/{homeid}")]
        public async Task<IActionResult> GetHomeEndDates(int homeid, [FromBody] Home_OverallS_DateDto s_DateDto)
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
            var homeenddates = new List<String>();
            var allHomeEndDates = await _home_overallDAO.getHomeEndDates(s_DateDto , homeid);
            if (allHomeEndDates.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Homes either EndDates are found !!...");
                return NotFound(new { errors = error });
            }
            HashSet<String> uniqueValues = new HashSet<String>();

            foreach (var homeenddate in allHomeEndDates)
            {
                if (!uniqueValues.Contains(homeenddate))
                {
                    homeenddates.Add(homeenddate);
                    uniqueValues.Add(homeenddate);

                }
            }
            return Ok(homeenddates);
        }

        [HttpGet("data/{homeid}")]
        public async Task<IActionResult> GetHomeconsumption(int homeid, [FromBody] HomeDatesDto datesDto)
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
            var homedata = new List<Double>();
            var allHomeconsumptionvals = await _home_overallDAO.getHomeconsumption(datesDto, homeid);
            if (allHomeconsumptionvals.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Home Consumptions are found !!...");
                return NotFound(new { errors = error });
            }


            foreach (var value in allHomeconsumptionvals)
            {
                homedata.Add(value);
            }
            return Ok(homedata);
        }

    }
}