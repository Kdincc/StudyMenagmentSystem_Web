using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.ViewModels;

namespace Task10.UI.Controllers
{
    public class StudentsController(IStudentsService studentsService) : Controller
    {
        private readonly IStudentsService _studentsService = studentsService;

        public async Task<IActionResult> Index()
        {
            IEnumerable<StudentDto> students = await _studentsService.GetStudentsWithGroupsNameAsync();

            return View(new StudentListViewModel { Students = students });
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<GroupDto> groups = await _studentsService.GetGroupsAsync();

            return View(new CreateStudentViewModel { Groups = groups });
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string lastName, int groupId)
        {
            await _studentsService.CreateStudentAsync(name, lastName, groupId);

            return RedirectToAction("Index");
        }
    }
}
