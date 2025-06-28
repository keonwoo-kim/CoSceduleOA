using CoScheduleOA.Interfaces.Providers;
using CoScheduleOA.Models.Reddit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoScheduleOA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RedditController : ControllerBase
    {
        private readonly IRedditClientProvider _reddit;

        public RedditController(IRedditClientProvider reddit)
        {
            _reddit = reddit;
        }

        [HttpGet("search")]
        public async Task<ActionResult<RedditSearchResult>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { error = "Please add a query input" });
            }

            try
            {
                var result = await _reddit.SearchAsync(query);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
