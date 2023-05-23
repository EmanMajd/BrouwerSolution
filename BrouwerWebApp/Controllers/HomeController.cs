using BrouwerWebApp.Models;
using BrouwerWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrouwerWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		readonly IBrouwerRepository repository;
		public HomeController(ILogger<HomeController> logger, IBrouwerRepository repository)
		{
			_logger = logger;
			this.repository = repository;
		}

		public async Task<IActionResult> Index() => View(await repository.FindAllAsync());

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