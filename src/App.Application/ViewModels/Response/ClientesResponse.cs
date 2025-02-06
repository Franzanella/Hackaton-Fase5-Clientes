using App.Domain.Models;
using System;

namespace App.Application.ViewModels.Response
{
    public class ClientesResponse
    {
        public ClientesResponse(ClienteBD _cliente)
        {

            Id = _cliente.Id;
            Cpf = _cliente.Cpf;
            Email = _cliente.Email;
            Password = _cliente.Password;
            DataCadastro = _cliente.DataCadastro;

        }
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
