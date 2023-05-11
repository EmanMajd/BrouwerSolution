
using Microsoft.EntityFrameworkCore;
using BrouwerService.Models;
using global::BrouwerService.Models;

namespace BrouwerService.Repositories;
public class BierlandContext : DbContext
{
	public BierlandContext(DbContextOptions<BierlandContext> 
		options) : base(options) { }


	public  DbSet<Brouwer> Brouwers { init; get; }
}