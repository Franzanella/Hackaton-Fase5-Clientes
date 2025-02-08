using TechTalk.SpecFlow;
using Api.Controllers;
using App.Application.Interfaces;
using App.Application.ViewModels.Response;
using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.BDD.StepDefinitions
{
    [Binding]
    public class BuscaUsuariosStepDefinitions
    {
        private ClientesController _controller;
        private readonly Mock<IClientesService> _appService = new();
        private ClienteBD _fakeCliente;
        private readonly ScenarioContext _scenarioContext;

        // Construtor para injetar o ScenarioContext
        public BuscaUsuariosStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            _appService.Setup(service => service.GetById(It.IsAny<string>()))
                .ReturnsAsync((string cpf) =>
                {
                    // Simula o comportamento do GetById: retorna um cliente fictício se o CPF corresponder.
                    return cpf == _fakeCliente.Cpf ? new ClientesResponse(_fakeCliente) : null;
                });

            _controller = new ClientesController(_appService.Object);
        }

        [Given(@"que existe um cliente com CPF (.*)")]
        public void GivenQueExisteUmClienteComCpf(string cpf)
        {
            // Adiciona um cliente fictício.
            _fakeCliente = new ClienteBD
            {
                Cpf = cpf,
                Id = 1,
                Email = "cliente@example.com",
                Password = "senha123",
                DataCadastro = System.DateTime.Now
            };
        }

        [When(@"eu faço uma busca pelo CPF (.*)")]
        public async Task WhenEuFaçoUmaBuscaPeloCpf(string cpf)
        {
            // Chama o método do controller para buscar o cliente
            _controller = new ClientesController(_appService.Object);
            var result = await _controller.BuscaUsuarios(cpf);
            _scenarioContext.Set(result);  // Usando _scenarioContext ao invés de ScenarioContext.Current
        }
    }
}
