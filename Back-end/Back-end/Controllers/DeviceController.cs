using Back_end.DTOS.Device;
using Back_end.DTOS.Room;
using Back_end.DTOS.Validation.DeviceValidation;
using Back_end.DTOS.Validation.RoomValidation;
using Back_end.Services.DeviceService;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
         private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }


        #region Create
        [HttpPost]
        public async Task<ActionResult> CreateDevice([FromBody] DeviceCreateDto deviceCreateDto)
        {
            DeviceCreateValidation validationRules = new DeviceCreateValidation();
            var validatorResults = await validationRules.ValidateAsync(deviceCreateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var device = await _deviceService.AddRoom(deviceCreateDto);
            if (!device.massage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(device.massage);
                return NotFound(new { erroes = error });

            }
            if (!device.massageBadRequst.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(device.massageBadRequst);
                return BadRequest(new { erroes = error });

            }
            return Ok(new { massage = "Room added ", Device = device });
        }
        #endregion

        #region Update
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateDevice([FromBody] DeviceUpdateDto deviceUpdateDto, int Id)
        {
            DeviceUpdateValidation validationRules = new DeviceUpdateValidation();
            var validatorResults = await validationRules.ValidateAsync(deviceUpdateDto);
            if (!validatorResults.IsValid)
            {
                var errorMessages = validationRules.ListError(validatorResults);
                return BadRequest(new { errors = errorMessages });
            }
            var device = await _deviceService.UpdateDevice(deviceUpdateDto, Id);
            if (!device.massage.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(device.massage);
                return NotFound(new { erroes = error });
            }
            if (!device.massageBadRequst.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(device.massageBadRequst);
                return BadRequest(new { erroes = error });

            }
            return Ok(new { massage = "Device Is Updated ", Device = device });
        }
        #endregion

        #region Delete

        [HttpPut("remove/{id}")]
        public async Task<ActionResult> RemoveDevice(int Id)
        {
            string result = await _deviceService.RemoveDevice(Id);
            if (!result.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(result);
                return NotFound(new { erroes = error });
            }
            return Ok("Room Is Deleted");
        }
        #endregion

    }
}
