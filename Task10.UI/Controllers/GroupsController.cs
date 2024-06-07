using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.ViewModels;

namespace Task10.UI.Controllers
{
    public sealed class GroupsController(IGroupsService groupsService) : Controller
    {
        private readonly IGroupsService _groupsService = groupsService;

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            IEnumerable<GroupDto> groups = await _groupsService.GetGroupsWithCourseNamesAsync(cancellationToken);

            return View(new GroupsListViewModel { Groups = groups });
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool isDeleted = await _groupsService.DeleteGroupAsync(id, cancellationToken);

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Can`t delete non empty groups!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateGroup(CancellationToken cancellationToken)
        {
            IEnumerable<CourseDto> courses = await _groupsService.GetCoursesAsync(cancellationToken);

            return View(new CreateGroupViewModel { Courses = courses });
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupViewModel groupViewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _groupsService.CreateGroupAsync(groupViewModel.Name, groupViewModel.CourseId, cancellationToken);

                return RedirectToAction("Index");
            }

            groupViewModel.Courses = await _groupsService.GetCoursesAsync(cancellationToken);

            return View(groupViewModel);
        }


        public async Task<IActionResult> EditGroup(int id, CancellationToken cancellationToken)
        {
            GroupEditDto groupEditDto = await _groupsService.GetEditGroupDtoAsync(id, cancellationToken);

            return View(new EditGroupViewModel
            {
                Name = groupEditDto.Group.Name,
                Courses = groupEditDto.Courses,
                Id = groupEditDto.Group.Id,
                CourseId = groupEditDto.Group.CourseId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(EditGroupViewModel viewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _groupsService.EditGroupAsync(viewModel.Name, viewModel.Id, viewModel.CourseId, cancellationToken);

                return RedirectToAction("Index");
            }

            viewModel.Courses = await _groupsService.GetCoursesAsync(cancellationToken);

            return View(viewModel);
        }
    }
}
