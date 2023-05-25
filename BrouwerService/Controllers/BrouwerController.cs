using BrouwerService.Models;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

namespace BrouwerService.Controllers;

[Route("brouwers")]
[ApiController]
public class BrouwerController : ControllerBase
{
	readonly IBrouwerRepository repository;
	readonly IHttpClientFactory clientFactory;
	public BrouwerController(IBrouwerRepository repository, IHttpClientFactory clientFactory)
	{
		this.repository = repository;
		this.clientFactory = clientFactory;
	}

	
	[SwaggerOperation("Alle brouwers")] [HttpGet]
	public async Task<ActionResult> FindAll() => 
		base.Ok(await repository.FindAllAsync()); [HttpGet("{id}")]
	
	[SwaggerOperation("Brouwer waarvan je de id kent")]
	public async Task<ActionResult> FindById(int id) => 
		await repository.FindByIdAsync(id) is Brouwer brouwer ? base.Ok(brouwer) :
			  base.NotFound(); 
	
	[HttpGet("naam")]
	[SwaggerOperation("Brouwers waarvan je het begin van de naam kent")]
	public async Task<ActionResult> FindByBeginNaam(string begin) =>
							base.Ok(await repository.FindByBeginNaamAsync(begin)); [HttpDelete("{id}")]

	[SwaggerOperation("Brouwer verwijderen")]
	public async Task<ActionResult> Delete(int id)
	{
		var brouwer = await repository.FindByIdAsync(id);
		if (brouwer == null)
		{
			return base.NotFound();
		}
		await repository.DeleteAsync(brouwer);
		return base.Ok();
	}

	[SwaggerOperation("Brouwer toevoegen")]
	[HttpPost]
	public async Task<ActionResult> Post(Brouwer brouwer)
	{
		if (ModelState.IsValid)
		{
			await repository.InsertAsync(brouwer);
			return base.CreatedAtAction(nameof(FindById), new { id = brouwer.Id }, null);
		}
		return base.BadRequest(ModelState);
	}
	[SwaggerOperation("Brouwer wijzigen")]
	[HttpPut("{id}")]
	public async Task<ActionResult> Put(int id, Brouwer brouwer)
	{
		if (ModelState.IsValid)
		{
			try
			{
				brouwer.Id = id;
				await repository.UpdateAsync(brouwer);
				return base.Ok();
			}
			catch (DbUpdateConcurrencyException)
			{
				return base.NotFound();
			}
			catch
			{
				return base.Problem();
			}
		}
		return base.BadRequest(ModelState);
	}
}
