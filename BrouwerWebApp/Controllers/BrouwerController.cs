using BrouwerWebApp.Repositories;
using BrouwerWebApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace BrouwerWebApp.Controllers;
[Route("brouwers"), ApiController]
public class BrouwerController : ControllerBase
{
	readonly IBrouwerRepository repository;
	public BrouwerController(IBrouwerRepository repository) =>
	this.repository = repository;
	[HttpGet("{id}")]
	public async Task<ActionResult> FindById(int id) =>
	await repository.FindByIdAsync(id) is Brouwer brouwer ? base.Ok(brouwer) :
	base.NotFound();
}