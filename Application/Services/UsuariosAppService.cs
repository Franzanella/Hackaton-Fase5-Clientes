using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModel.Request;
using Application.ViewModel.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;

namespace Application.Services
{
    public class UsuariosAppService : IUsuariosAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosAppService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
            return null;
        }
    }
}
