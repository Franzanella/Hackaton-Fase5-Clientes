using System.Threading.Tasks;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace App.Tests.Repositories
{
    public class ClientesRepositoryTests
    {
        private readonly DbContextOptions<MySQLContext> _dbContextOptions;

        public ClientesRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<MySQLContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task PostCliente_ShouldAddClienteToDatabase()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new ClientesRepository(context);

            var cliente = new ClienteBD
            {
                Cpf = "41512369020",
                Email = "teste@gmail.com",
                Password = "Teste123"
            };

            // Act
            await repository.PostCliente(cliente);

            // Assert
            var savedCliente = await context.Clientes.FirstOrDefaultAsync(p => p.Cpf == "41512369020");
            Assert.NotNull(savedCliente);
            Assert.Equal("41512369020", savedCliente.Cpf);
            Assert.Equal("teste@gmail.com", savedCliente.Email);
            Assert.Equal("Teste123", savedCliente.Password);
        }

        [Fact]
        public async Task GetClientes_ShouldReturnCliente_WhenCpfExists()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new ClientesRepository(context);

            var cliente = new ClienteBD
            {
                Cpf = "41512369020",
                Email = "teste@gmail.com",
                Password = "Teste123"
            };

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetClientes("41512369020");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("41512369020", result.Cpf);
            Assert.Equal("teste@gmail.com", result.Email);
            Assert.Equal("Teste123", result.Password);
        }

        [Fact]
        public async Task GetClientes_ShouldReturnNull_WhenCpfDoesNotExist()
        {
            // Arrange
            using var context = new MySQLContext(_dbContextOptions);
            var repository = new ClientesRepository(context);

            // Act
            var result = await repository.GetClientes("99999999999");

            // Assert
            Assert.Null(result);
        }

    }
}
