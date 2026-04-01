using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuantityMeasurementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new 
            { 
                message = "Quantity Measurement API is running!",
                timestamp = DateTime.UtcNow,
                status = "healthy"
            });
        }
        
        [HttpGet("auth-test")]
        [Authorize]  // This will require authentication
        public IActionResult AuthTest()
        {
            return Ok(new 
            { 
                message = "You are authenticated!",
                user = User.Identity?.Name,
                timestamp = DateTime.UtcNow
            });
        }
    }
}