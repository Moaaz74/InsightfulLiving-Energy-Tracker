﻿using Back_end.DTOS.Home;
using Back_end.DTOS.Room;
using Back_end.DTOS.Validation.HomeValidation;
using Back_end.DTOS.Validation.RoomValidation;
using Back_end.Models;
using Back_end.Services.HomeService;
using Back_end.Services.RoomService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        public readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        #region Create
        [HttpPost]
        public async Task<ActionResult> CreateHome([FromBody] RoomCreateDto roomCreateDto)
        {
            RoomCreateValidation validationRules = new RoomCreateValidation();
            var validatorResults = await validationRules.ValidateAsync(roomCreateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var room = await _roomService.AddRoom(roomCreateDto);
            if (!room.massage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(room.massage);
                return NotFound(new { erroes = error });

            }
            if (!room.massageBadRequst.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(room.massageBadRequst);
                return BadRequest(new { erroes = error });

            }
            return Ok(new { massage = "Room added ", Room = room });
        }
        #endregion

        #region Update
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateHome([FromBody] RoomUpdateDto roomUpdateDto , int Id)
        {
            RoomUpdateValidation validationRules = new RoomUpdateValidation();
            var validatorResults = await validationRules.ValidateAsync(roomUpdateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var room = await _roomService.UpdateRoom(roomUpdateDto,Id);
            if (!room.massage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(room.massage);
                return NotFound(new { erroes = error });
            }
            if (!room.massageBadRequst.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(room.massageBadRequst);
                return BadRequest(new { erroes = error });

            }
            return Ok(new { massage = "Room Is Updated ", Room = room });
        }
        #endregion

        #region Delete

        [HttpPut("remove/{id}")]
        public async Task<ActionResult> RemoveRoom(int Id)
        {
            string result = await _roomService.RemoveRoom(Id);
            if (!result.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(result);
                return NotFound(new { erroes = error });
            }
            return Ok("Room Is Deleted");
        }
        #endregion

        #region GetHomeWithRoom 

        [HttpGet("RoomDetalis/{id}")]
        public async Task<IActionResult> GetHomeWithRooms(int id)
        {
            var homeWithRooms = await _roomService.GetRoomWithDevicesAndHome(id);

            if (homeWithRooms == null)
            {
                List<string> error = new List<string>();
                error.Add("Room Is Not Exist");
                return NotFound(new { erroes = error });
            }

            return Ok(homeWithRooms);
        }
        #endregion

        #region ViewRoom

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomViewDto>> ViewHome(int id)
        {
            var room = await _roomService.ViewRoom(id);
            if (room == null)
            {
                List<string> error = new List<string>();
                error.Add("Room Is Not Exist");
                return NotFound(new { erroes = error });
            }

            return Ok(room);
        }
        #endregion

        #region All-Room deleted or not 

        [HttpGet("All-Room")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewAllHome()
        {
            var rooms = await _roomService.ViewsRoom();
            if (rooms == null)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Rooms");
                return NotFound(new { erroes = error });
            }
            return Ok(rooms);
        }

        #endregion

        [HttpGet]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHome()
        {
            var rooms = await _roomService.ViewsRoomNotDelete();
            if (rooms == null || (rooms.Count == 0))
            {
                List<string> error = new List<string>();
                error.Add("Not Found Rooms");
                return NotFound(new { erroes = error });
            }
            
            return Ok(rooms);
        }


        [HttpGet("Deleted-Homes")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHomeDelete()
        {
            var rooms = await _roomService.ViewsRoomDelete();
            if (rooms == null)
            {
                List<string> error = new List<string>();
                error.Add("Not Found Rooms");
                return NotFound(new { erroes = error });
            }
            return Ok(rooms);
        }
    }
} 