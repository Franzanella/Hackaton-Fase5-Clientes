using System.Threading.Tasks;
using Application.ViewModel.Request;
using Application.ViewModel.Response;
using Microsoft.AspNetCore.Identity.Data;

namespace Application.Interfaces
{
    public interface IUsuariosAppService
    {
        Task<UsuarioByIdRequest> PostUsuarios(UsuariosResquest filtro);
        Task<UsuariosResponse> GetById(UsuarioByIdRequest filtro);
        Task<LoginResponse> Authenticate(LoginRequest request);

    }
}
