using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Home;
using CoScheduleOA.Models.Ratings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoScheduleOA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAll([FromQuery] int itemId)
        {
            var list = await _ratingService.GetByItemAsync(itemId);
            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult<RatingDto>> GetById([FromQuery] int id)
        {
            var dto = await _ratingService.FindAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost] 
        public async Task<ActionResult<RatingDto>> Create([FromBody] RatingCreateModel model)
        {
            var dto = await _ratingService.CreateAsync(model);
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RatingUpdateModel model)
        {
            var success = await _ratingService.UpdateAsync(model);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var success = await _ratingService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}