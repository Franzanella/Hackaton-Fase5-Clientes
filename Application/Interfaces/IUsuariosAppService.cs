using System.Threading.Tasks;
using Application.ViewModel.Request;
using Application.ViewModel.Response;


namespace Application.Interfaces
{
    public interface IUsuariosAppService
    {
        Task<UsuarioByIdRequest> PostUsuarios(UsuariosResquest filtro);
        Task<UsuariosResponse> GetById(UsuarioByIdRequest filtro);
        Task<LoginResponse> Authenticate(ViewModel.Request.LoginRequest request);

    }
}
