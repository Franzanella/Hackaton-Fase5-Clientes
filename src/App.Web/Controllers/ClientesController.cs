using System.Net;
using App.Application.Interfaces;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("Clientes/")]
    public class ClientesController : ControllerBase
    {

        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }


        #region POST/clientes
        [SwaggerResponse(201, "A solicitação foi atendida e resultou na criação de um ou mais novos recursos.")]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido a sintaxe malformada!")]
        [SwaggerResponse(401, "Requisição requer autenticação do usuário!")]
        [SwaggerResponse(403, "Privilégios insuficientes!")]
        [SwaggerResponse(404, "O recurso solicitado não existe!")]
        [SwaggerResponse(400, "Condição prévia dada em um ou mais dos campos avaliado como falsa!")]
        [SwaggerResponse(500, "Servidor encontrou uma condição inesperada!")]
        [HttpPost("")]
        [SwaggerOperation(
       Summary = "Endpoint para criação de um novo cliente.",
       Description = @"Endpoint para criar um novo cliente.</br></br>
                            <b>Parâmetros de entrada:</b></br></br>
                              &bull; <b>cpf</b>:  Cpf do cliente. &rArr; <font color='red'><b>Obrigatório</b></font><br>
                              &bull; <b>senha</b>: Senha do cliente. &rArr; <font color='red'><b>Obrigatório</b></font><br>
                              &bull; <b>email</b>: email do cliente. &rArr; <font color='red'><b>Obrigatório</b></font><br>
    ", Tags = new[] { "Clientes" }
   )]
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody] PostCliente input)
        {
            var rnt = await _clientesService.PostCliente(input);
            return StatusCode((int)HttpStatusCode.Created, new { id = rnt });

        }
        #endregion

        #region get/clientes/cpf
        [HttpGet("{cpf}")]
        [SwaggerOperation(
              Summary = "EndPoint para buscar um usuario no Cognito via api-gateway e lambda",
              Description = @" </br>
                          <b>Parâmetros de entrada:</b>
                        <br/> &bull; <b>CPF</b>:cpf do usuario &rArr; <font color='red'><b>Obrigatorio</b></font>"
                           ,
               Tags = ["Clientes"]
            )]

        [SwaggerResponse(200, "Consulta executada com sucesso!", typeof(ClientesResponse))]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido a sintaxe malformada!", null)]
        [SwaggerResponse(404, "O recurso solicitado não existe!", null)]
        [SwaggerResponse(412, "Condição prévia dada em um ou mais dos campos avaliado como falsa!", null)]
        [SwaggerResponse(500, "Servidor encontrou uma condição inesperada!", null)]

        public async Task<IActionResult> BuscaUsuarios([FromRoute] string cpf)
        {
            var rtn = await _clientesService.GetById(cpf);
            if (rtn == null)
            {
                return NoContent();
            }

            return Ok(rtn);
        }
        #endregion

        #region POST/clientes/login
        [HttpPost("login")]
        [SwaggerOperation(
                Summary = "Autentica um cliente e retorna um token JWT.",
                Description = @"Endpoint para login do cliente.</br></br>
                    <b>Parâmetros:</b></br>
                    &bull; <b>cpf</b>: CPF do cliente (obrigatório).<br>
                    &bull; <b>password</b>: Senha do cliente (obrigatório).",
             Tags = new[] { "Clientes" }
        )]
        [SwaggerResponse(200, "Autenticação bem-sucedida!", typeof(TokenResponse))]
        [SwaggerResponse(401, "CPF ou senha inválidos!")]
        public async Task<IActionResult> Login([FromBody] ClienteRequest request)
        {
            var response = await _clientesService.LoginCliente(request.Cpf, request.Password);

            if (response == null)
                return Unauthorized("CPF ou senha inválidos.");

            return Ok(response);
        }
        #endregion

    }
}