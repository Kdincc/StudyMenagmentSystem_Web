using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.ViewModels;

namespace Task10.UI.Controllers
{
    public sealed class StudentsController(IStudentsService studentsService) : Controller
    {
        private readonly IStudentsService _studentsService = studentsService;

        public async Task<IActionResult> Index()
        {
            IEnumerable<StudentDto> students = await _studentsService.GetStudentsWithGroupsNameAsync();

            return View(new StudentListViewModel { Students = students });
        }

        public async Task<IActionResult> Edit(int id)
        {
            StudentEditDto studentEditDto = await _studentsService.GetEditStudentDtoAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentsService.EditStudentAsync(studentViewModel.Name, studentViewModel.LastName, studentViewModel.Id, studentViewModel.GroupId);

                return RedirectToAction("Index");
            }

            studentViewModel.Groups = await _studentsService.GetGroupsAsync();

            return View(studentViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _studentsService.DeleteStudentAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<GroupDto> groups = await _studentsService.GetGroupsAsync();

            return View(new CreateStudentViewModel { Groups = groups });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentsService.CreateStudentAsync(viewModel.Name, viewModel.LastName, viewModel.GroupId);

                return RedirectToAction("Index");
            }

            viewModel.Groups = await _studentsService.GetGroupsAsync();

            return View(viewModel);

        }
    }
}
