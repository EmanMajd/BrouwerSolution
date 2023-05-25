

// delete brouwer
/*using System.Net;
Console.Write("Id:");
var id = int.Parse(Console.ReadLine()!);
using var client = new HttpClient(); 
var response = await client.DeleteAsync($"http://localhost:5000/brouwers/{id}");
switch (response.StatusCode) 
{
case HttpStatusCode.OK: 
Console.WriteLine("Brouwer is verwijderd.");
	break;
case HttpStatusCode.NotFound: 
Console.WriteLine("Brouwer niet gevonden");
	break;
	default:
Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
	break;
}*/

// get brouwer
/*using System.Net;
Console.Write("Id:");
var id = int.Parse(Console.ReadLine()!);
using var client = new HttpClient();
var response = await client.GetAsync($"http://localhost:5000/brouwers/{id}"); 
switch (response.StatusCode)
{
	case HttpStatusCode.OK:
		var brouwer = await response.Content.ReadAsAsync<Brouwer>(); 
Console.WriteLine(brouwer.Naam); 
break;
	case HttpStatusCode.NotFound:
		Console.WriteLine("Brouwer niet gevonden");
		break;
	default:
		Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
		break;
}*/

// get all brouwers
/*
using System.Net;
Console.Write("Druk enter om de namen te zien");
Console.ReadLine();
using var client = new HttpClient();
var response = await client.GetAsync($"http://localhost:5000/brouwers");
switch (response.StatusCode)
{
	case HttpStatusCode.OK:
		var brouwers = await response.Content.ReadAsAsync<List<Brouwer>>();
		brouwers.ForEach(brouwer => Console.WriteLine(brouwer.Naam));
		break;
	case HttpStatusCode.NotFound:
		Console.WriteLine("Brouwer niet gevonden");
		break;
	default:
		Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
		break;
}*/

// Brouwers waarvan de naam begint met een stuk tekst
/*
using System.Net;
Console.Write("Begin van de naam:");
var begin = Console.ReadLine();
using var client = new HttpClient();
var response = await client.GetAsync($"http://localhost:5000/brouwers/naam?begin={begin}");
switch (response.StatusCode)
{
	case HttpStatusCode.OK:
		var brouwers = await response.Content.ReadAsAsync<List<Brouwer>>();
		brouwers.ForEach(brouwer => Console.WriteLine(brouwer.Naam));
		break;
	case HttpStatusCode.NotFound:
		Console.WriteLine("Brouwer niet gevonden");
		break;
	default:
		Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
		break;
}*/

// brouwer toevoegen
using BrouwerWebApp.Controllers;
using MassTransit.Clients;
using Microsoft.AspNetCore.Mvc.Testing;
using ServiceStack;
using System.Net;
Console.Write("Naam:");
var naam = Console.ReadLine()!;
Console.Write("Postcode:");
var postcode = int.Parse(Console.ReadLine()!);
Console.Write("Gemeente:");
var gemeente = Console.ReadLine()!;
var brouwer = new Brouwer() { Naam = naam, Postcode = postcode, Gemeente = gemeente };
using var client =  new HttpClient();
//var clientFactory = new WebApplicationFactory<HomeController>();

//using var client = clientFactory.CreateClient();
var response =
await client.PostAsJsonAsync("http://localhost:5000/brouwers", brouwer); 
if (response.StatusCode == HttpStatusCode.Created) 
{
	Console.WriteLine("Brouwer is toegevoegd.");
	Console.WriteLine($"Zijn URI is {response.Headers.Location}"); 
}
else
{
	Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
}