using System.Threading.Tasks;
using App.Domain.Models;

namespace App.Domain.Interfaces
{
    public interface IClientesRepository
    {
        Task<ClienteBD> GetClientes(string cpf);
        Task PostCliente(ClienteBD Cliente);

    }
}
