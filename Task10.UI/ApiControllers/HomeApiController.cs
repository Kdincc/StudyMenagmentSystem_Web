using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.UI.ApiControllers
{
    [ApiController]
    public sealed class HomeApiController(IHomeService homeService) : ControllerBase
    {
        private readonly IHomeService _homeService = homeService;

        [Route("api/home/all")]
        [HttpGet]
        [ProducesResponseType<IEnumerable<Course>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            HomeDto dto = await _homeService.GetHomeDtoAsync(token);

            return Ok(dto.Courses);
        }
    }
}
