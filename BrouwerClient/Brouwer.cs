using System.ComponentModel.DataAnnotations;


public class Brouwer
{ 
	public int Id { get; set; }
	[Required] 
	public string Naam { get; set; }
	public int Postcode { get; set; }
	[Required]
	public string Gemeente { get; set; }

}
