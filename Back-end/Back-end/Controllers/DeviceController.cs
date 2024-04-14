using Back_end.DTOS.Device;
using Back_end.DTOS.Home;
using Back_end.DTOS.Room;
using Back_end.DTOS.Validation.DeviceValidation;
using Back_end.DTOS.Validation.RoomValidation;
using Back_end.Services.DeviceService;
using Confluent.Kafka;
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
        [HttpPost("Create")]
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
                return NotFound(new { errors = error });

            }
            if (!device.massageBadRequst.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(device.massageBadRequst);
                return BadRequest(new { errors = error });

            }
            return Ok(device);
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
                return NotFound(new { errors = error });
            }
            if (!device.massageBadRequst.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(device.massageBadRequst);
                return BadRequest(new { errors = error });

            }
            return Ok(device);
        }
        #endregion

        #region Delete

        [HttpDelete("remove/{id:int}")]
        public async Task<ActionResult> RemoveDevice(int Id)
        {
            string result = await _deviceService.RemoveDevice(Id);
            if (!result.IsNullOrEmpty())
            {
                List<string> error = new List<string>();
                error.Add(result);
                return NotFound(new { errors = error });
            }
            return Ok(new { massage= "Device Is Deleted" });
        }
        #endregion

        #region GetDeviceWithRoom 

        [HttpGet("DeviceDetalis/{id}")]
        public async Task<IActionResult> GetHomeWithRooms(int id)
        {
            var device = await _deviceService.GetDeviceWithRoom(id);

            if (device == null)
            {
                List<string> error = new List<string>();
                error.Add("Device Is Not Exist");
                return NotFound(new { errors = error });
            }

            return Ok(device);
        }
        #endregion

        #region ViewDevice

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DeviceViewDto>> ViewDevice(int id)
        {
            var device = await _deviceService.ViewDevice(id);
            if (device == null)
            {
                List<string> error = new List<string>();
                error.Add("Room Is Not Exist");
                return NotFound(new { errors = error });
            }

            return Ok(device);
        }
        #endregion

        #region All-Device deleted or not 

        [HttpGet("All-Device")]
        public async Task<ActionResult<List<DeviceViewDto>>> ViewAllDevice()
        {
            var devices = await _deviceService.ViewsDevice();
            if (devices == null || (devices.Count()==0))
            {
                List<string> error = new List<string>();
                error.Add("Not Found Devices");
                return NotFound(new { erroes = error });
            }
            return Ok(devices);
        }

        #endregion

        #region ViewsDevice
        [HttpGet]
        public async Task<ActionResult<List<DeviceViewDto>>> ViewsDevice()
        {
            var devices = await _deviceService.ViewsDeviceNotDelete();
            if (devices == null || (devices.Count == 0))
            {
                List<string> error = new List<string>();
                error.Add("Not Found Devices");
                return NotFound(new { errors = error });
            }
            return Ok(devices);
        }
        #endregion

        #region Deleted-Devieces
        [HttpGet("Deleted-Devieces")]
        public async Task<ActionResult<List<HomeViewsDto>>> ViewsHomeDelete()
        {
            var devices = await _deviceService.ViewsDeviceDelete();
            if (devices == null || (devices.Count == 0))
            {
                List<string> error = new List<string>();
                error.Add("Not Found Rooms");
                return NotFound(new { errors = error });
            }
            return Ok(devices);
        }
        #endregion
    }
}
