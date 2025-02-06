using App.Domain.Models;
using App.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly MySQLContext _dbContext;

        public ClientesRepository(MySQLContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task PostCliente(ClienteBD Cliente)
        {
            _dbContext.Clientes.Add(Cliente);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<ClienteBD> GetClientes(string cpf)
        {
            return await _dbContext.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }


    }
}
