using App.Domain.Models;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public interface IClientesRepository
    {
        Task<ClienteBD> GetClientes(string cpf);
        Task PostCliente(ClienteBD Cliente);

    }
}
