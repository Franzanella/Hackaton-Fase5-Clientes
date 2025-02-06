using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IClientesService
    {
        Task<TokenResponse> LoginCliente(string cpf, string password);
        Task<ClientesResponse> GetById(string cpf);
        Task<int> PostCliente(PostCliente input);

    }
}
