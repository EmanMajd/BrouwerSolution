using Microsoft.EntityFrameworkCore;
using BrouwerWebApp.Models;
namespace BrouwerWebApp.Repositories;
public class BierlandContext : DbContext
{
	public BierlandContext(DbContextOptions<BierlandContext> options) : base(options) { }
	public  DbSet<Brouwer> Brouwers { init; get; }
}