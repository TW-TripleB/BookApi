using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        [HttpGet("me")]
        public IActionResult GetUserInfo()
        {
            var username = User.Identity?.Name ?? "Unknown";
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new
            {
                Username = username,
                Claims = claims
            });
        }
    }
}