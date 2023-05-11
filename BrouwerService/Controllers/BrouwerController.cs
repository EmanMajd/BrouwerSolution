using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrouwerService.Controllers;

[Route("brouwers")]
[ApiController]
public class BrouwerController : ControllerBase
{
	readonly IBrouwerRepository repository;
	public BrouwerController(IBrouwerRepository repository) =>
	this.repository = repository;
}
