using BrouwerService.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using BrouwerService.Models;
using System.Text.Json;
using BrouwerService.Controllers;

namespace BrouwerServiceIntegrationTests;
[TestClass]
public class BrouwerControllerTest
{
	HttpClient client = null!;
	Mock<IBrouwerRepository> mock = null!;
	Brouwer brouwer1 = null!;
	[TestInitialize]
	public void TestInitialize()
	{
		mock = new Mock<IBrouwerRepository>(); 
		var repository = mock.Object; 
		var factory = new WebApplicationFactory<BrouwerController>();
		client = factory.WithWebHostBuilder(builder =>
		builder.ConfigureTestServices(services =>
		services.AddScoped<IBrouwerRepository>(_ => repository))) 
		.CreateClient();
		brouwer1 = new Brouwer { Id = 1, Naam = "1", Postcode = 1000, Gemeente = "1" };
	}
	[TestMethod]
	public void DeleteMetBestaandeBrouwerGeeftOK()
	{
		mock.Setup(repo => repo.FindByIdAsync(1))
		.Returns(Task.FromResult((Brouwer?)brouwer1)); 
		var response = client.DeleteAsync("brouwers/1").Result; 
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		mock.Verify(repo => repo.DeleteAsync(brouwer1)); 
	}

	[TestMethod]
	public void DeleteMetOnbestaandeBrouwerGeeftNotFound()
	{
		var response = client.DeleteAsync("brouwers/-1").Result;
		Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		mock.Verify(repo => repo.DeleteAsync(It.IsAny<Brouwer>()), Times.Never);
	}
	[TestMethod]
	public void GetMetOnbestaandeBrouwerGeeftNotFound()
	{
		var response = client.GetAsync("brouwers/-1").Result;
		Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		mock.Verify(repo => repo.FindByIdAsync(-1));
	}

	[TestMethod]
	public void GetMetBestaandeBrouwerGeeftOK()
	{
		mock.Setup(repo => repo.FindByIdAsync(1)).Returns(Task.FromResult<Brouwer?>(brouwer1));
		var response = client.GetAsync("brouwers/1").Result;
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		mock.Verify(repo => repo.FindByIdAsync(1));
		var body = response.Content.ReadAsStringAsync().Result; 
		var document = JsonDocument.Parse(body); 
		Assert.AreEqual(1, document.RootElement.GetProperty("id").GetInt32()); 
		Assert.AreEqual("1", document.RootElement.GetProperty("naam").GetString()); 
	}
}