using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using App.Domain.Interfaces;
using App.Domain.Models;
using Application.Services;
using Moq;
using Xunit;

namespace App.Tests.Services
{
    public class ClientesServiceTests
    {
        private readonly Mock<IClientesRepository> _mockRepository;
        private readonly ClienteService _service;
        public readonly string _fakeSecretKey;

        public ClientesServiceTests()
        {
            _mockRepository = new Mock<IClientesRepository>();
            _service = new ClienteService(_mockRepository.Object);
            _fakeSecretKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        #region LOGIN jwt
        [Fact]
        public async Task LoginCliente_DeveRetornarToken_QuandoCredenciaisCorretas()
        {
            // Arrange
            string cpf = "41512369020";
            string senha = "Teste123";
            string senhaHash = HashPassword(senha);

            var cliente = new ClienteBD { Cpf = cpf, Password = senhaHash };
            _mockRepository.Setup(repo => repo.GetClientes(cpf)).ReturnsAsync(cliente);

            // Act
            var resultado = await _service.LoginCliente(cpf, senha);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<TokenResponse>(resultado);
            var tokenHandler = new JwtSecurityTokenHandler();
            Assert.True(tokenHandler.CanReadToken(resultado.Token));
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarNull_QuandoCpfIncorreto()
        {
            // Arrange
            string cpf = "41512369020";
            string senha = "Teste123";

            _mockRepository.Setup(repo => repo.GetClientes(cpf)).ReturnsAsync((ClienteBD)null);

            // Act
            var resultado = await _service.LoginCliente(cpf, senha);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarNull_QuandoSenhaIncorreta()
        {
            // Arrange
            string cpf = "41512369020";
            string senhaCorreta = "Teste123";
            string senhaErrada = "SenhaErrada";
            string senhaHash = HashPassword(senhaCorreta);

            var cliente = new ClienteBD { Cpf = cpf, Password = senhaHash };
            _mockRepository.Setup(repo => repo.GetClientes(cpf)).ReturnsAsync(cliente);

            // Act
            var resultado = await _service.LoginCliente(cpf, senhaErrada);

            // Assert
            Assert.Null(resultado);
        }

        #endregion

        #region getBycpf
        [Fact]
        public async Task GetByCpf_WhenClienteExists()
        {
            // Arrange
            var cpf = "41512369020";
            var clienteBD = new ClienteBD
            {
                Id = 1,
                Cpf = cpf,
                Email = "teste@gmail.com",
                Password = "hashedpassword"
            };

            _mockRepository.Setup(repo => repo.GetClientes(cpf))
                .ReturnsAsync(clienteBD);

            // Act
            var result = await _service.GetById(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cpf, result.Cpf);
            Assert.Equal(clienteBD.Email, result.Email);
        }

        [Fact]
        public async Task GetByCpf_Null_WhenClienteDoesNotExist()
        {
            // Arrange
            var cpf = "41512369020";
            _mockRepository.Setup(repo => repo.GetClientes(cpf))
                .ReturnsAsync((ClienteBD)null);

            // Act
            var result = await _service.GetById(cpf);

            // Assert
            Assert.Null(result);
        }
        #endregion


        #region POST/clientes
        [Fact]
        public async Task PostCLiente_WithValidInput()
        {
            // Arrange
            var input = new PostCliente
            {
                Cpf = "41512369020",
                Email = "teste@gmail.com",
                Password = "Teste123"
            };

            // Act
            await _service.PostCliente(input);

            // Assert
            _mockRepository.Verify(r => r.PostCliente(It.Is<ClienteBD>(p =>
                p.Cpf == input.Cpf &&
                p.Email == input.Email &&
                p.Password == HashPassword(input.Password))), Times.Once);
        }

        // Método auxiliar para simular o hash da senha
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        #endregion



    }
}
