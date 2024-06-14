using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;

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
        public async Task<IActionResult> EditStudent(int studentId, int groupId, string name, string lastName, CancellationToken cancellationToken)
        {
            await _studentsService.EditStudentAsync(name, lastName, studentId, groupId, cancellationToken);

            return NoContent();
        }

        [HttpPost("create/{name}/{lastName}/{groupId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStudent(string name, string lastName, int groupId, CancellationToken cancellationToken)
        {
            await _studentsService.CreateStudentAsync(name, lastName, groupId, cancellationToken);

            return Created();
        }

        [HttpDelete("delete/{studentId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGroup(int studentId, CancellationToken cancellationToken)
        {
            await _studentsService.DeleteStudentAsync(studentId, cancellationToken);

            return NoContent();
        }

    }
}
