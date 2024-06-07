using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.Models;
using Task10.UI.ViewModels;

namespace Task10.UI.Controllers
{
    public sealed class HomeController(IHomeService homeService) : Controller
    {
        private readonly IHomeService _homeService = homeService;

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            HomeDto homeDto = await _homeService.GetHomeDtoAsync(cancellationToken);

            return View(new HomeViewModel
            {
                Courses = homeDto.Courses,
                Groups = homeDto.Groups,
                Students = homeDto.Students
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
