using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModel.Request;
using Application.ViewModel.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class UsuariosAppService : IUsuariosAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;

        public UsuariosAppService(IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;

        }


        private string GeneratePasswordHash(string password, string salt)
        {

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }


        private bool VerifyPasswordHash(string password, string salt, string storedHash)
        {
            var hash = GeneratePasswordHash(password, salt);
            return hash == storedHash;
        }

        public async Task<UsuarioByIdRequest> PostUsuarios(UsuariosResquest filtro)
        {
            var salt = Guid.NewGuid().ToString();
            var passwordHash = GeneratePasswordHash(filtro.Senha, salt);

            var usuario = new Usuario(
                filtro.Nome,
                filtro.Email,
                filtro.Login,
                passwordHash,
                salt
            );

            await _usuarioRepository.PostUsuario(usuario);

            return new UsuarioByIdRequest
            {
                IdUsuario = usuario.IdUsuario
            };
        }

        public async Task<UsuariosResponse> GetById(UsuarioByIdRequest filtro)
        {
            var usuario = await _usuarioRepository.GetUsuario(filtro.IdUsuario);
            if (usuario == null) return null;

            return new UsuariosResponse(usuario);
        }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorLogin(request.Login);

            if (usuario == null || !VerifyPasswordHash(request.Senha, usuario.Salt, usuario.Senha))
            {
                return null;
            }

            var token = GerarToken(usuario);

            return new LoginResponse
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = token
            };
        }

        private string GerarToken(Domain.Entities.Usuario usuario)
        {
            var chaveSecreta = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var credenciais = new SigningCredentials(new SymmetricSecurityKey(chaveSecreta), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    

}
}
