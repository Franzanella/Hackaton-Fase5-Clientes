﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using App.Domain.Interfaces;
using App.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class ClienteService : IClientesService
    {
        private readonly IClientesRepository _repository;
        private static readonly string _secretKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("MINHA_CHAVE_SECRETA_FIXA"));


        public ClienteService(IClientesRepository repository)
        {
            _repository = repository;
        }

        #region get/cliente/cpf
        public async Task<ClientesResponse> GetById(string cpf)
        {
            var cliente = await _repository.GetClientes(cpf);
            if (cliente == null) return null;

            return new ClientesResponse(cliente);
        }
        #endregion

        #region Post/Cliente
        public async Task<int> PostCliente(PostCliente input)
        {
            ClienteBD novoCliente = new()
            {
                Cpf = input.Cpf,
                Email = input.Email,
                Password = HashPassword(input.Password)
            };

            await _repository.PostCliente(novoCliente);
            return novoCliente.Id;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        #endregion

        #region Autenticacao JWT
        public async Task<TokenResponse> LoginCliente(string cpf, string password)
        {
            var cliente = await _repository.GetClientes(cpf);
            if (cliente == null || !VerificarSenha(password, cliente.Password))
                return null;

            return GerarTokenJwt(cliente);
        }

        private bool VerificarSenha(string senhaDigitada, string senhaHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));
                var hashSenhaDigitada = Convert.ToBase64String(hashBytes);
                return hashSenhaDigitada == senhaHash;
            }
        }



        private TokenResponse GerarTokenJwt(ClienteBD cliente)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = DateTime.UtcNow.AddDays(30);

            var token = new JwtSecurityToken(
                issuer: "AppAPI",
                audience: "AppClientes",
                claims: new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, cliente.Cpf),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: expiracao,
                signingCredentials: credentials
            );

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                DuracaoToken = (int)TimeSpan.FromDays(30).TotalSeconds,
                DataHoraExpiracao = expiracao
            };
        }

        #endregion
    }
}
