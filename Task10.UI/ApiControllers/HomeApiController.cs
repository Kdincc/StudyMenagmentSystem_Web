using Microsoft.AspNetCore.Mvc;
using Task10.Core.Aggregates;
using Task10.Core.Interfaces;

namespace Task10.UI.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController(IHomeService homeService) : ControllerBase
    {
        private readonly IHomeService _homeService = homeService;

        [HttpGet("courses")]
        [ProducesResponseType<СourseList>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourses(CancellationToken token)
        {
            СourseList list = await _homeService.GetCourseListAsync(token);

            return Ok(list);
        }
    }
}
