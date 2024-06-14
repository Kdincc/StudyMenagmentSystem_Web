using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.ViewModels;

namespace Task10.UI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("students")]
    public sealed class StudentsController(IStudentsService studentsService) : Controller
    {
        private readonly IStudentsService _studentsService = studentsService;

        [HttpGet("")]
        [HttpGet("index")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            IEnumerable<StudentDto> students = await _studentsService.GetStudentsWithGroupsNameAsync(cancellationToken);

            return View(new StudentListViewModel { Students = students });
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            StudentEditDto studentEditDto = await _studentsService.GetEditStudentDtoAsync(id, cancellationToken);

            return View(
                new EditStudentViewModel
                {
                    Name = studentEditDto.Student.Name,
                    LastName = studentEditDto.Student.LastName,
                    GroupId = studentEditDto.Student.GroupId,
                    Groups = studentEditDto.Groups,
                    Id = studentEditDto.Student.Id
                });
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(EditStudentViewModel studentViewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _studentsService.EditStudentAsync(studentViewModel.Name, studentViewModel.LastName, studentViewModel.Id, studentViewModel.GroupId, cancellationToken);

                return RedirectToAction("Index");
            }

            studentViewModel.Groups = await _studentsService.GetGroupsAsync(cancellationToken);

            return View(studentViewModel);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            DeleteStudentDto dto = await _studentsService.GetDeleteStudentDto(id, cancellationToken);

            return View(new DeleteStudentViewModel() { Name = dto.Name, LastName = dto.LastName, Id = dto.Id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(DeleteStudentViewModel viewModel, CancellationToken cancellationToken)
        {
            await _studentsService.DeleteStudentAsync(viewModel.Id, cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            IEnumerable<GroupDto> groups = await _studentsService.GetGroupsAsync(cancellationToken);

            return View(new CreateStudentViewModel { Groups = groups });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStudentViewModel viewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _studentsService.CreateStudentAsync(viewModel.Name, viewModel.LastName, viewModel.GroupId, cancellationToken);

                return RedirectToAction("Index");
            }

            viewModel.Groups = await _studentsService.GetGroupsAsync(cancellationToken);

            return View(viewModel);
        }
    }
}
