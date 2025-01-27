using Domain.Interfaces;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class DataBaseModuleDependency
    {
        public static void AddDataBaseModule(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuariosRepository>();


        }
    }
}

