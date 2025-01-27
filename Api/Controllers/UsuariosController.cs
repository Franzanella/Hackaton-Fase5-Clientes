using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModel.Request;
using Application.ViewModel.Response;
using Domain.Base;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("usuarios/")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosAppService _usuarioService;

        public UsuariosController(IUsuariosAppService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        #region  POST/usuarios/auth/login 
        [HttpPost("auth/login")]
        [SwaggerResponse(200, "Login realizado com sucesso!", typeof(LoginResponse))]
        [SwaggerResponse(400, "A solicitacao nao pode ser entendida pelo servidor devido a sintaxe malformada!")]
        [SwaggerResponse(401, "Requisicao requer autenticacao do usuario!")]
        [SwaggerResponse(403, "Privilegios insuficientes!")]
        [SwaggerResponse(404, "O recurso solicitado nao existe!")]
        [SwaggerResponse(412, "Condicao previa dada em um ou mais dos campos avaliado como falsa!")]
        [SwaggerResponse(500, "Servidor encontrou uma condicao inesperada!")]
        [SwaggerOperation(
         Summary = "Endpoint para autenticar usuario.",
         Description = @"Endpoint para autenticar um usuario.</br></br>
                            <b>Parametros de entrada:</b></br></br>
                             &bull; <b>Login</b>:  Login do cliente. &rArr; <font color='red'><b>Obrigatorio</b></font><br>
                         &bull; <b>Senha</b>: Senha do cliente. &rArr; <font color='red'><b>Obrigatorio</b></font><br>
                        
                       
",
         Tags = new[] { "Usuarios" }
     )]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _usuarioService.Authenticate(request);

                if (result == null)
                {
                    return Unauthorized("Credenciais inválidas.");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
        }



        #endregion

        #region POST/usuarios
        [SwaggerResponse(201, "A solicitacao foi atendida e resultou na criacao de um ou mais novos recursos.", typeof(UsuariosResponse))]
        [SwaggerResponse(400, "A solicitacao nao pode ser entendida pelo servidor devido a sintaxe malformada!")]
        [SwaggerResponse(401, "Requisicao requer autenticacao do usuario!")]
        [SwaggerResponse(403, "Privilegios insuficientes!")]
        [SwaggerResponse(404, "O recurso solicitado nao existe!")]
        [SwaggerResponse(412, "Condicao previa dada em um ou mais dos campos avaliado como falsa!")]
        [SwaggerResponse(500, "Servidor encontrou uma condicao inesperada!")]
        [HttpPost("")]
        [SwaggerOperation(
         Summary = "Endpoint para criacao de usuarios na base de dados.",
         Description = @"Endpoint para cadastrar um novo usuario.</br></br>
                            <b>Parametros de entrada:</b></br></br>
                             &bull; <b>Nome</b>:  Nome do cliente. &rArr; <font color='red'><b>Obrigatorio</b></font><br>
                         &bull; <b>Email</b>: Email do cliente. &rArr; <font color='red'><b>Obrigatorio</b></font><br>
                         &bull; <b>Login</b>: Login do cliente. &rArr; <font color='red'><b>Obrigatorio</b></font><br>
                         &bull; <b>Senha</b>: Senha do cliente. &rArr; <font color='red'><b>Obrigatorio</b></font><br>
                       
",
         Tags = new[] { "Usuarios" }
     )]
        [Consumes("application/json")]
        public async Task<IActionResult> PostUsuario([FromBody] UsuariosResquest filtro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rtn = await _usuarioService.PostUsuarios(filtro);
                return CreatedAtAction(nameof(PostUsuario), new { id = rtn.IdUsuario }, rtn);
            }
            catch (CustomValidationException ex)
            {
                var errorResponse = ex.Error;
                return StatusCode(412, errorResponse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
        }

        #endregion

        #region GET/usuarios/{idUsuario}
        [SwaggerResponse(200, "Consulta executada com sucesso!", typeof(UsuariosResponse))]
        [SwaggerResponse(204, "Requisicao concluida, porem nao ha dados de retorno!")]
        [SwaggerResponse(206, "Conteudo parcial!", typeof(IList<UsuariosResponse>))]
        [SwaggerResponse(412, "condicao previa dada em um ou mais dos campos avaliado como falsa.", typeof(ErrorValidacao))]
        [HttpGet("{idUsuario}")]
        [SwaggerOperation(
         Summary = "Busca informacoes de cadastro de um determinado usuário.",
         Description = @"Endpoint para buscar informacoes de um determinado usuário. A busca pode ser feita pelo filtro abaixo:</br></br>
                            <b>Parametros de entrada:</b></br></br>
                             &bull; <b>idUsuario</b>:  ID do usuário cadastrado. &rArr; <font color='red'><b>Obrigatorio</b></font>",
         Tags = new[] { "Usuários" }
     )]
        [Consumes("application/json")]
        public async Task<IActionResult> GetUsuarios([FromRoute] UsuarioByIdRequest filtro)
        {

            try
            {
                var rtn = await _usuarioService.GetById(filtro);
                if (rtn == null)
                {
                    return NoContent();
                }

                return Ok(rtn);
            }
            catch (CustomValidationException ex)
            {
                var errorResponse = ex.Error;
                return StatusCode(412, errorResponse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }

        }
        #endregion


    }
}