using BrouwerService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add context and register services
builder.Services.AddDbContext<BierlandContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("bierland"))); 
builder.Services.AddScoped<IBrouwerRepository, BrouwerRepository>();
builder.Services.AddScoped<IFiliaalRepository, FiliaalRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger(c =>
	c.PreSerializeFilters.Add((swagger, request) =>
	swagger.Servers = new List<OpenApiServer>
	{ new OpenApiServer { Url = $"{request.Scheme}://{request.Host.Value}" } }));
	app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
