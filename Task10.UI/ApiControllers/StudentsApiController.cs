using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

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

        [HttpPut("edit/{studentId:int}/{groupId:int}/{name}/{lastName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditStudent(int studentId, int groupId, string name, string lastName, CancellationToken cancellationToken)
        {
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

        [HttpPost("create/{name}/{lastName}/{groupId:int}")]
        [ProducesResponseType<Student>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateStudent(string name, string lastName, int groupId, CancellationToken cancellationToken)
        {
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
