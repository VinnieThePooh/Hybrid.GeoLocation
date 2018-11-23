using Microsoft.AspNetCore.Mvc;

namespace Hybrid.GeoLocation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        public GeoController()
        {

        }
        
        public ActionResult<string> Get()
        {
            return "You are in Russian, man";
        }

        [HttpGet("ip/{ip}")]
        public ActionResult<string> GetByIp(string ip)
        {
            return "You are in Russian, man";
        }
    }
}