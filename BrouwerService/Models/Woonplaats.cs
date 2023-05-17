using System.ComponentModel.DataAnnotations;

namespace BrouwerService.Models;

public class Woonplaats
{
	public int Id { init; get; }
	public int Postcode { init; get; }
	[Required]
	public string Naam { init; get; }

	public  List<Filiaal> Filialen { init; get; }
}
