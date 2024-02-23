﻿using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.DTOs.Cassandra_quries.Room_OverallDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Room_OverallController : ControllerBase
    {
        private readonly IRoom_OverallDAO _room_overallDAO;

        public Room_OverallController(IRoom_OverallDAO room_OverallDAO)
        {

            _room_overallDAO = room_OverallDAO;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoom_Overall()
        {
            var room_OverallDtos = new List<Room_OverallDto>();
            var allRooms = await _room_overallDAO.getRoom();
            if (!allRooms.Any())
            {
                List<string> error = new List<string>();
                error.Add("There is no rooms consumption yet...");
                return NotFound(new { errors = error });
            }

            Room_OverallDto roomDto;


            foreach (var room in allRooms)
            {
                roomDto = new Room_OverallDto();
                roomDto.Start = room.start;
                roomDto.End = room.end;
                roomDto.RoomConsumption = room.roomconsumption;
                roomDto.RoomId = room.roomid;
                roomDto.EnergyType = room.energytype;
                room_OverallDtos.Add(roomDto);
            }
            return Ok(room_OverallDtos);
        }

        [HttpGet("{roomid}")]
        public async Task<IActionResult> GetRoomStartDates(int roomid, [FromBody] object energyType)
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
            var roomstartdates = new List<String>();
            var allRoomStartDates = await _room_overallDAO.getRoomStartDates(energytype, roomid);
            if (allRoomStartDates.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Rooms either StartDates are found !!...");
                return NotFound(new { errors = error });
            }
            HashSet<String> uniqueValues = new HashSet<String>();

            foreach (var roomstartdate in allRoomStartDates)
            {
                if (!uniqueValues.Contains(roomstartdate))
                {
                    roomstartdates.Add(roomstartdate);
                    uniqueValues.Add(roomstartdate);
                }
            }
            return Ok(roomstartdates);
        }



        [HttpGet("EndDates/{roomid}")]
        public async Task<IActionResult> GetRoomEndDates(int roomid, [FromBody] Room_OverallS_DateDto s_DateDto)
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
            var roomenddates = new List<String>();
            var allRoomEndDates = await _room_overallDAO.getRoomEndDates(s_DateDto, roomid);
            if (allRoomEndDates.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Rooms either EndDates are found !!...");
                return NotFound(new { errors = error });
            }
            HashSet<String> uniqueValues = new HashSet<String>();

            foreach (var roomenddate in allRoomEndDates)
            {
                if (!uniqueValues.Contains(roomenddate))
                {
                    roomenddates.Add(roomenddate);
                    uniqueValues.Add(roomenddate);

                }
            }
            return Ok(roomenddates);
        }

        [HttpGet("data/{roomid}")]
        public async Task<IActionResult> GetRoomconsumption(int roomid, [FromBody] RoomDatesDto datesDto)
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
            var roomdata = new List<Double>();
            var allRoomconsumptionvals = await _room_overallDAO.getRoomconsumption(datesDto, roomid);
            if (allRoomconsumptionvals.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add("No Room Consumptions are found !!...");
                return NotFound(new { errors = error });
            }


            foreach (var value in allRoomconsumptionvals)
            {
                roomdata.Add(value);
            }
            return Ok(roomdata);
        }


    }
}

