
using Back_end.DAOs.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLController : ControllerBase
    {
        private readonly IMLDOAs mLDOAs;

        public MLController(IMLDOAs mLDOAs)
        {
            this.mLDOAs = mLDOAs;
        }


        [HttpGet("{homeid}")]
        public async Task<IActionResult> GetLastFire(int homeid)
        {
            var result = await mLDOAs.getLastFire(homeid);

            if (result == null || (result.Count() == 0))
            {
                return Ok(new { massage = "Not FireDetect" });
            }

            return Ok(result);

        }
    }
}
