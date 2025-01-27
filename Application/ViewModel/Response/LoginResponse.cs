using System;

namespace Application.ViewModel.Response
{

    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiraEm { get; set; }
        public UsuarioResponse Usuario { get; set; }
    }

    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }

}

