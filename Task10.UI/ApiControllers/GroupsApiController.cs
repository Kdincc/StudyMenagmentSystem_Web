using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;
using Task10.UI.ViewModels;

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

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditGroup([FromBody] EditGroupViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int courseId = viewModel.CourseId;
            int groupId = viewModel.Id;
            string name = viewModel.Name;

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

        [HttpPost("create")]
        [ProducesResponseType<Group>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupViewModel createGroupViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isCourseNotExists = !await _groupsService.IsCourseExistsAsync(createGroupViewModel.CourseId, cancellationToken);

            if (isCourseNotExists)
            {
                return NotFound($"Course with that id {createGroupViewModel.CourseId} not found!");
            }

            await _groupsService.CreateGroupAsync(createGroupViewModel.Name, createGroupViewModel.CourseId, cancellationToken);

            return CreatedAtAction(nameof(CreateGroup), new Group() { Name = createGroupViewModel.Name, CourseId = createGroupViewModel.CourseId });
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
