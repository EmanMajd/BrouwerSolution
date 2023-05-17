using System.ComponentModel.DataAnnotations;

namespace BrouwerService.Models;

public class Filiaal
{
	public int Id { init; get; }
	[Required]
	public  string Naam { init; get; }
	public int HuurPrijs { init; get; }
	public int WoonPlaatsId { init; get; }
	[Required]
	public  Woonplaats Woonplaats { init; get; }
}
