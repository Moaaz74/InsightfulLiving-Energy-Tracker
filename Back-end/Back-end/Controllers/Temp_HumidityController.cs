using Back_end.DAOs.Implementations;
using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries.Temp_HumidityDtos;
using Back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Temp_HumidityController : ControllerBase
    {
        private readonly ITemp_HumidityDAO _temp_humidityDAO;

        public Temp_HumidityController(ITemp_HumidityDAO temp_humidityDAO)
        {

            _temp_humidityDAO = temp_humidityDAO;
        }

        [HttpGet]
        public async Task<IActionResult> GetTemp_Humidity()
        {
            var temp_humidityDtos = new List<Temp_HumidityDto>();
            var allDegrees = await _temp_humidityDAO.getTemp_Humidity();
            if (!allDegrees.Any())
            {
                List<string> error = new List<string>();
                error.Add("There is no rooms consumption yet...");
                return NotFound(new { errors = error });
            }

            Temp_HumidityDto temp_humidityDto;


            foreach (var degree in allDegrees)
            {
                temp_humidityDto = new Temp_HumidityDto();
                temp_humidityDto.HomeId = degree.homeid;
                temp_humidityDto.SensorId = degree.sensorid;
                temp_humidityDto.DateTime = degree.datetime;
                temp_humidityDto.Temperature = degree.temperature;
                temp_humidityDto.Humidity = degree.humidity;
                temp_humidityDtos.Add(temp_humidityDto);
            }
            return Ok(temp_humidityDtos);
        }

    }
}
