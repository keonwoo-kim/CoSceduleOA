using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoScheduleOA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll(int itemId)
        {
            var list = await _commentService.GetByItemAsync(itemId);
            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult<CommentDto>> GetById(int id)
        {
            var dto = await _commentService.FindAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> Create(CommentCreateModel model)
        {
            var dto = await _commentService.CreateAsync(model);
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CommentUpdateModel model)
        {
            var success = await _commentService.UpdateAsync(model);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _commentService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}