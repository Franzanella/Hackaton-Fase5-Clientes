using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Controllers;
using App.Application.Interfaces;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace App.Test._1_WebAPI.Controllers
{
    public class ClientesControllerTests
    {
        public ClientesController _controller;
        private readonly Mock<IClientesService> _appServive = new();
        private readonly List<Cliente> _fakeProdutos = new List<Cliente>();

        public ClientesControllerTests()
        {
            _controller = new ClientesController(_appServive.Object);


            for (int i = 1; i < 10; i++)
            {
                _fakeProdutos.Add(new Cliente(new ClienteBD(
                    )));
            }
        }


        #region [GET]
        [Fact(DisplayName = "BuscaUsuarios - Retorna Ok")]
        public async Task BuscaUsuarios_Returns_Ok()
        {
            // Arrange
            var cpf = "41512369020";
            var clienteBD = new ClienteBD
            {
                Id = 1,
                Email = "teste@gmail.com",
                Cpf = cpf,
                Password = "teste@123",
                DataCadastro = System.DateTime.Now
            };

            var clienteResponse = new ClientesResponse(clienteBD);

            _appServive.Setup(service => service.GetById(cpf))
                .ReturnsAsync(clienteResponse);

            // Act
            var result = await _controller.BuscaUsuarios(cpf);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ClientesResponse>(okResult.Value);
            Assert.Equal(cpf, response.Cpf);
        }

        [Fact(DisplayName = "BuscaUsuarios - Retorna NoContent")]
        public async Task BuscaUsuarios_Returns_NoContent()
        {
            // Arrange
            var cpf = "41512369020";
            _appServive.Setup(service => service.GetById(cpf))
                .ReturnsAsync((ClientesResponse)null);

            // Act
            var result = await _controller.BuscaUsuarios(cpf);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        #endregion

        #region [POST]

        [Fact(DisplayName = "PostClientes Ok")]
        public async Task PostClientes_ReturnsCreatedResult()
        {
            // Arrange
            var item = new PostCliente
            {
                Cpf = "41512369020",
                Email = "teste@gmail.com",
                Password = "Teste123"
            };

            var retornoEsperado = 123;
            _appServive.Setup(service => service.PostCliente(It.IsAny<PostCliente>())).ReturnsAsync(retornoEsperado);

            // Act
            var result = await _controller.Post(item);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, objectResult.StatusCode);
            Assert.Equal(retornoEsperado, ((dynamic)objectResult.Value).id);
        }

        #endregion

        #region POST LOGIN JWT
        [Fact(DisplayName = "Login - Retorna Ok com Token")]
        public async Task Login_Returns_Ok_With_Token()
        {
            // Arrange
            var request = new ClienteRequest
            {
                Cpf = "41512369020",
                Password = "Teste123"
            };

            var expectedResponse = new TokenResponse
            {
                Token = "jwt-token"
            };

            _appServive.Setup(service => service.LoginCliente(request.Cpf, request.Password))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<TokenResponse>(okResult.Value);
            Assert.Equal(expectedResponse.Token, response.Token);
        }

        [Fact(DisplayName = "Login - Retorna Unauthorized quando CPF ou Senha são inválidos")]
        public async Task Login_Returns_Unauthorized_When_Invalid_Credentials()
        {
            // Arrange
            var request = new ClienteRequest
            {
                Cpf = "41512369020",
                Password = "SenhaErrada"
            };

            _appServive.Setup(service => service.LoginCliente(request.Cpf, request.Password))
                .ReturnsAsync((TokenResponse)null);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("CPF ou senha inválidos.", unauthorizedResult.Value);
        }
        #endregion


    }
}
