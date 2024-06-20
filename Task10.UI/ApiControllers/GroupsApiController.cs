using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditGroup(int groupId, string name, int courseId, CancellationToken cancellationToken)
        {
            bool isCourseNotExists = !await _groupsService.IsCourseExistsAsync(courseId, cancellationToken);
            bool isGroupNotExists = !await _groupsService.IsGroupExistsAsync(groupId, cancellationToken);

            if (isCourseNotExists)
            {
                return NotFound($"Course with that id {courseId} not found!");
            }

            if (isGroupNotExists)
            {
                return NotFound($"Group with that id {groupId} not found!");
            }

            await _groupsService.EditGroupAsync(name, groupId, courseId, cancellationToken);

            return NoContent();
        }

        [HttpPost("create/{name}/{courseId:int}")]
        [ProducesResponseType<Group>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateGroup(string name, int courseId, CancellationToken cancellationToken)
        {
            bool isCourseNotExists = !await _groupsService.IsCourseExistsAsync(courseId, cancellationToken);
            
            if (isCourseNotExists) 
            {
                return NotFound($"Course with that id {courseId} not found!");
            }

            await _groupsService.CreateGroupAsync(name, courseId, cancellationToken);

            return CreatedAtAction(nameof(CreateGroup), new Group() { Name = name, CourseId = courseId });
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGroup(int id, CancellationToken cancellationToken)
        {
            bool isGroupNotExists = !await _groupsService.IsGroupExistsAsync(id, cancellationToken);

            if (isGroupNotExists) 
            {
                return NotFound($"Group with that id {id} not found!");
            }

            bool isDeleted = await _groupsService.DeleteGroupAsync(id, cancellationToken);

            if (isDeleted)
            {
                return NoContent();
            }

            return BadRequest("Only groups with no students can be deleted!");
        }


    }
}
