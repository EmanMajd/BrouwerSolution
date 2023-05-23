using BrouwerWebApp.Models;
using Microsoft.EntityFrameworkCore;
namespace BrouwerWebApp.Repositories;
public class BrouwerRepository : IBrouwerRepository
{
	readonly BierlandContext context;
	public BrouwerRepository(BierlandContext context) => this.context = context;
	public async Task<List<Brouwer>> FindAllAsync() =>
	await context.Brouwers.AsNoTracking().ToListAsync();
	public async Task<Brouwer?> FindByIdAsync(int id) =>
	await context.Brouwers.FindAsync(id);
}