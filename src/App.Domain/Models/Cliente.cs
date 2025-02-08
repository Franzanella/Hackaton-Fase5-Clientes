using System;
using App.Domain.Validations;

namespace App.Domain.Models
{
    public class ClienteBD
    {

        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;


        public ClienteBD()
        {
        }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Cpf, "O cpf não pode estar vazio!");

        }
    }
}
