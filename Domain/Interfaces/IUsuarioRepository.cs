using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {

        Task PostUsuario(Usuario cliente);

        Task<Usuario> GetUsuario(int idUsuario);

    }
}
