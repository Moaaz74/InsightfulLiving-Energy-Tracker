using Back_end.DAOs.Interfaces;
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


        [HttpGet("All")]
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

        [HttpGet("last/{roomid}")]
        public async Task<IActionResult> GetLastRoom_Overall(int roomid , [FromQuery] string energyType)
        {

            if (energyType == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No EnergyType is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            var Room = await _room_overallDAO.getLastRoom(roomid,energyType);
            if (Room == null)
            {
                List<string> error = new List<string>();
                error.Add("There is no room consumption yet...");
                return NotFound(new { errors = error });
            }

            Room_OverallDto roomDto = new Room_OverallDto();
                roomDto.Start = Room.start;
                roomDto.End = Room.end;
                roomDto.RoomConsumption = Room.roomconsumption;
                roomDto.RoomId = Room.roomid;
            roomDto.EnergyType = Room.energytype;

            return Ok(roomDto);
        }

        [HttpGet("StartDates/{roomid}")]
        public async Task<IActionResult> GetRoomStartDates(int roomid, [FromQuery] string energyType)
        {

            if (energyType == string.Empty)
            {
                List<string> error = new List<string>();
                error.Add("No EnergyType is specified !!!!...");
                return BadRequest(new { errors = error });
            }
            var roomstartdates = new List<String>();
            var allRoomStartDates = await _room_overallDAO.getRoomStartDates(energyType, roomid);
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
        public async Task<IActionResult> GetRoomEndDates(int roomid, [FromQuery] string StartDate , [FromQuery] string EnergyType)
        {

            Room_OverallS_DateDto s_DateDto = new Room_OverallS_DateDto()
            {
                energyType = EnergyType,
                startDate = StartDate
            };
          
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
        public async Task<IActionResult> GetRoomconsumption(int roomid, [FromQuery] string StartDate 
            , [FromQuery] string EndDate , [FromQuery] string EnergyType)
        {

            RoomDatesDto datesDto = new RoomDatesDto()
            {
                startDate = StartDate,
                energyType = EnergyType,
                endDate = EndDate
            };

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

