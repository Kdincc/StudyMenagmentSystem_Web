using Microsoft.AspNetCore.Mvc;
using Task10.Core.Aggregates;
using Task10.Core.Interfaces;

namespace Task10.UI.ApiControllers
{
    [ApiController]
    public class HomeApiController(IHomeApiService homeService) : ControllerBase
    {
        private readonly IHomeApiService _homeService = homeService;

        [Route("api/home/courses")]
        [HttpGet]
        [ProducesResponseType<СourseList>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourses(CancellationToken token)
        {
            СourseList list = await _homeService.GetCourses(token);

            return Ok(list);
        }
    }
}
