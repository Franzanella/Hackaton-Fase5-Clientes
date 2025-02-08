using App.Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace APICliente.Tests
{
    internal class MoroniWebApplication : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            _ = builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IClientesService, ClienteService>();
            });
        }
    }
    public class ProgramTests
    {

        [Fact]
        public async Task ShoudReturns200_WhenGetValidPath()
        {
            // Arrange
            await using var application = new MoroniWebApplication();
            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/Clientes/41512369020");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }


}
