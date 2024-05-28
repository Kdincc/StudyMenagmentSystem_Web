using Microsoft.AspNetCore.Mvc;
using Task10.Core.Interfaces;

namespace Task10.UI.Controllers
{
    public sealed class GroupsController(IGroupsService groupsService) : Controller
    {
        private readonly IGroupsService _groupsService = groupsService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
