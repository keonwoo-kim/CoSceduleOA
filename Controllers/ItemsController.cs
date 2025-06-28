using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoScheduleOA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost("ensure")]
        public async Task<ActionResult<int>> Ensure([FromBody] ItemCreateModel model)
        {
            var id = await _itemService.SaveIfNotExistsAndReturnIdAsync(model);

            return Ok(id);
        }
    }
}
