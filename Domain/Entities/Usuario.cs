using Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Usuario
    {
        public Usuario(string nome, string email, string login, string senha, string salt)
        {
            Nome = nome;
            Email = email;
            Login = login;
            Senha = senha;
            Salt = salt;
            DataCadastro = DateTime.Now;
            ValidateEntity();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Salt { get; set; }

        public DateTime DataCadastro { get; set; }

        #region Validations

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Login, "O login é obrigatório!");
            AssertionConcern.AssertArgumentNotEmpty(Senha, "A senha é obrigatória!");
        }
        #endregion
    }
}
