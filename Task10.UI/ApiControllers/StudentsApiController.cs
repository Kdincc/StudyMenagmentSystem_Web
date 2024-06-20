using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;
using Task10.UI.ViewModels;

namespace Task10.UI.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsApiController(IStudentsService studentsService) : ControllerBase
    {
        private readonly IStudentsService _studentsService = studentsService;

        [HttpGet("students")]
        [ProducesResponseType<IEnumerable<StudentDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudents(CancellationToken cancellationToken)
        {
            IEnumerable<StudentDto> students = await _studentsService.GetStudentsWithGroupsNameAsync(cancellationToken);

            return Ok(students);
        }

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int studentId = viewModel.Id;
            int groupId = viewModel.GroupId;
            string name = viewModel.Name;
            string lastName = viewModel.LastName;

            bool isStudentNotExists = !await _studentsService.IsStudentExistsAsync(studentId, cancellationToken);
            bool isGroupNotExists = !await _studentsService.IsGroupExistsAsync(groupId, cancellationToken);

            if (isStudentNotExists)
            {
                return NotFound($"Student with that id {studentId} not found!");
            }

            if (isGroupNotExists)
            {
                return NotFound($"Group with that id {groupId} not found!");
            }

            await _studentsService.EditStudentAsync(name, lastName, studentId, groupId, cancellationToken);

            return NoContent();
        }

        [HttpPost("create")]
        [ProducesResponseType<Student>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int groupId = viewModel.GroupId;
            string name = viewModel.Name;
            string lastName = viewModel.LastName;

            bool isGroupNotExists = !await _studentsService.IsGroupExistsAsync(groupId, cancellationToken);

            if (isGroupNotExists)
            {
                return NotFound($"Group with that id {groupId} not found!");
            }

            await _studentsService.CreateStudentAsync(name, lastName, groupId, cancellationToken);

            return CreatedAtAction(nameof(CreateStudent), new Student() { Name = name, LastName = lastName, GroupId = groupId });
        }

        [HttpDelete("delete/{studentId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGroup(int studentId, CancellationToken cancellationToken)
        {
            bool isStudentNotExists = !await _studentsService.IsStudentExistsAsync(studentId, cancellationToken);

            if (isStudentNotExists)
            {
                return NotFound($"Student with that id {studentId} not found!");
            }

            await _studentsService.DeleteStudentAsync(studentId, cancellationToken);

            return NoContent();
        }

    }
}
