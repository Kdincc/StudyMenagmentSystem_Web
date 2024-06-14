using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;

namespace Task10.UI.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsApiController(IGroupsService groupsService) : ControllerBase
    {
        private readonly IGroupsService _groupsService = groupsService;

        [HttpGet("groups")]
        [ProducesResponseType<IEnumerable<GroupDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupList(CancellationToken cancellationToken)
        {
            IEnumerable<GroupDto> groups = await _groupsService.GetGroupsWithCourseNamesAsync(cancellationToken);

            return Ok(groups);
        }

        [HttpPut("edit/{groupId:int}/{name}/{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> EditGroup(int groupId, string name, int courseId, CancellationToken cancellationToken)
        {
            await _groupsService.EditGroupAsync(name, groupId, courseId, cancellationToken);

            return NoContent();
        }

        [HttpPost("create/{name}/{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGroup(string name, int courseId, CancellationToken cancellationToken)
        {
            await _groupsService.CreateGroupAsync(name, courseId, cancellationToken);

            return Created();
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGroup(int id, CancellationToken cancellationToken)
        {
            bool isDeleted = await _groupsService.DeleteGroupAsync(id, cancellationToken);

            if (isDeleted) 
            {
                return NoContent();
            }

            return BadRequest("Only groups with no students can be deleted!");
        }
    }
}
