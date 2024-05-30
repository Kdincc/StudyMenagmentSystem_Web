﻿using Microsoft.AspNetCore.Mvc;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.ViewModels;

namespace Task10.UI.Controllers
{
    public sealed class GroupsController(IGroupsService groupsService) : Controller
    {
        private readonly IGroupsService _groupsService = groupsService;

        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupDto> groups =  await _groupsService.GetGroupsWithAsync();

            return View(new GroupsListViewModel { Groups = groups });
        }

        public async Task<IActionResult> EditGroup(int id)
        {
            GroupEditDto groupEditDto = await _groupsService.GetEditGroupDto(id);

            return View(new EditGroupViewModel 
            { 
                Name = groupEditDto.Group.Name, 
                Courses = groupEditDto.Courses, 
                Id = groupEditDto.Group.Id,
                CourseId = groupEditDto.Group.CourseId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(EditGroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _groupsService.EditGroupAsync(viewModel.Name, viewModel.Id, viewModel.CourseId);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
