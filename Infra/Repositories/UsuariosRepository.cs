using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class UsuariosRepository : IUsuarioRepository
    {

        private readonly FiapDbContext _dbContext;

        public UsuariosRepository(FiapDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task PostUsuario(Usuario cliente)
        {
            await _dbContext.Usuarios.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Usuario> GetUsuario(int idUsuario)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
        }

        public async Task<Usuario> ObterPorLogin(string login)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        }

    }
}
