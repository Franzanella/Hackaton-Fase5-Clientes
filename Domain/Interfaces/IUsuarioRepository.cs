using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
       
        Task PostUsuario(Usuario cliente);

        Task<Usuario> GetUsuario(int idUsuario);

    }
}
