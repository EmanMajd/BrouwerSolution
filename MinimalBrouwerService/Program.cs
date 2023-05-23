using MinimalBrouwerService.Repositories;
using MinimalBrouwerService.Models;
using Microsoft.EntityFrameworkCore;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BierlandContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("bierland")));
builder.Services.AddScoped<IBrouwerRepository, BrouwerRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/brouwers", 
async (IBrouwerRepository repository) => 
await repository.FindAllAsync()); 
app.MapGet("/brouwers/{id}", 
async (int id, IBrouwerRepository repository) => 
await repository.FindByIdAsync(id) is Brouwer brouwer ? Results.Ok(brouwer) :
Results.NotFound());
app.MapGet("/brouwers/naam",
async (string begin,
IBrouwerRepository repository) =>
await repository.FindByBeginNaamAsync(begin));



app.MapDelete("/brouwers/{id}", VerwijderBrouwer);

app.MapPost("brouwers", VoegBrouwerToe);

app.Run();

async Task<IResult> VerwijderBrouwer(int id,
IBrouwerRepository repository)
{
	var brouwer = await repository.FindByIdAsync(id);
	if (brouwer == null)
	{
		return Results.NotFound();
	}
	await repository.DeleteAsync(brouwer);
	return Results.Ok();
}

async Task<IResult> VoegBrouwerToe(Brouwer brouwer, 
IBrouwerRepository repository)
{
	if (!MiniValidator.TryValidate(brouwer, out var errors))
	{
		return Results.BadRequest(errors);
	}
	else
	{
		await repository.InsertAsync(brouwer);
		return Results.Created($"brouwers/{brouwer.Id}", null);
	}
}