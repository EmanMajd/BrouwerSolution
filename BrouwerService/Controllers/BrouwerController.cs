using BrouwerService.Models;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrouwerService.Controllers;

[Route("brouwers")]
[ApiController]
public class BrouwerController : ControllerBase
{
	readonly IBrouwerRepository repository;
	public BrouwerController(IBrouwerRepository repository) =>
	this.repository = repository;

[HttpGet] 
public ActionResult FindAll() => base.Ok(repository.FindAll());

[HttpGet("{id}")] 
public ActionResult FindById(int id)  {
	var brouwer = repository.FindById(id); 
	return brouwer == null ? base.NotFound() : base.Ok(brouwer); 
}

	[HttpGet("naam")] 
	public ActionResult FindByBeginNaam(string begin) => base.Ok(repository.FindByBeginNaam(begin));

[HttpDelete("{id}")] 
public ActionResult Delete(int id) 
{
	var brouwer = repository.FindById(id); 
	if (brouwer == null)
	{
	return base.NotFound(); 
	}
	repository.Delete(brouwer); 
	return base.Ok(); 
	}

	[HttpPost] 
public ActionResult Post(Brouwer brouwer) 
{ 
		repository.Insert(brouwer); 
		return base.CreatedAtAction(
			nameof(FindById), 
			new { id = brouwer.Id}, 
			null); 
}

[HttpPut("{id}")] 
public ActionResult Put(int id, Brouwer brouwer) 
{
	try
	{
	brouwer.Id = id; 
	repository.Update(brouwer); 
	return base.Ok(); 
	} catch (DbUpdateConcurrencyException)
	{
	return base.NotFound(); 
	}
	catch
	{
		return base.Problem(); 
	}
}
}
