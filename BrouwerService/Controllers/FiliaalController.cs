using BrouwerService.DTOs;
using BrouwerService.Models;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BrouwerService.Controllers
{
	[Route("filialen"), ApiController]
	public class FiliaalController : ControllerBase
	{
		readonly IFiliaalRepository repository;
		public FiliaalController(IFiliaalRepository repository)
		=> this.repository = repository;
		[HttpGet, SwaggerOperation("Alle filialen")]
		public async Task<ActionResult> FindAll()
		{
			var filialen = await repository.FindAllAsync();
			var filiaalDTOs = filialen.Select(filiaal => new FiliaalDTO
			(
			filiaal.Id,
			filiaal.Naam,
			filiaal.Woonplaats.Postcode,
			filiaal.Woonplaats.Naam
			));
			return base.Ok(filiaalDTOs);
		}
	}
}
