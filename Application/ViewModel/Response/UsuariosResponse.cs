using Domain.Entities;

namespace Application.ViewModel.Response
{
    public class UsuariosResponse
    {
        public UsuariosResponse(Usuario _usuario)
        {
            Nome = _usuario.Nome;
            Email = _usuario.Email;
            Login = _usuario.Login;
            Senha = _usuario.Senha;
            DataCadastro = _usuario.DataCadastro.ToString();

        }

        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string DataCadastro { get; set; }


    }
}
