using Microsoft.AspNetCore.Mvc;
using Services;

namespace CameraSearchAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamerasController : ControllerBase
    {
        private ICameraService _cameraService;

        public CamerasController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? name)
        {
            var cameras = _cameraService.LoadCameras();

            if (!string.IsNullOrWhiteSpace(name))
            {
                cameras = cameras
                    .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Ok(cameras);
        }
    }
}
