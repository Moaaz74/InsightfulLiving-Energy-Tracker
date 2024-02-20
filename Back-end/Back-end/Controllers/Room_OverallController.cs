using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound(new { erroes = error });
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

    }
}

