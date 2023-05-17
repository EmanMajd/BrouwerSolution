using BrouwerService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrouwerService.Repositories;

public class FiliaalRepository : IFiliaalRepository
{
	readonly BierlandContext context;
	public FiliaalRepository(BierlandContext context) => this.context = context;
	public async Task<List<Filiaal>> FindAllAsync() =>
	await context.Filialen.Include(filiaal => filiaal.Woonplaats) .AsNoTracking().ToListAsync();
}
