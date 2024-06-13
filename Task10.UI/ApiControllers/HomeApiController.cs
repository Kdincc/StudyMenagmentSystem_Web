using Microsoft.AspNetCore.Mvc;
using Task10.Core.Aggregates;
using Task10.Core.Interfaces;

namespace Task10.UI.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController(IHomeApiService homeService) : ControllerBase
    {
        private readonly IHomeApiService _homeService = homeService;

        [HttpGet("courses")]
        [ProducesResponseType(typeof(СourseList), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourses(CancellationToken token)
        {
            СourseList list = await _homeService.GetCourses(token);

            return Ok(list);
        }
    }
}
