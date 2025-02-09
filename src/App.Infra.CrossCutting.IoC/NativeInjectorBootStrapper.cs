using App.Application.Interfaces;
using App.Domain.Interfaces;
using App.Domain.Models.External;
using App.Infra.Data.Context;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace App.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            ///     variables
            ///     
            services.AddSingleton<AwsConfig>(_ =>
               new AwsConfig
               {
                   S3Access = config["AWS_ACCESS_KEY_ID"],
                   S3Secret = config["AWS_SECRET_ACCESS_KEY"]
               });

            services.AddSingleton<Jwt>(_ =>
     new Jwt
     {
         jwt = "MINHA_CHAVE_SECRETA_FIXA" // Defina sua chave JWT fixa aqui
     });



            ////=======================================================================
            ///
            ///  INSTACIAS DE SERVICES
            /// 
            ///

            services.AddScoped<IClientesService, ClienteService>();
            ////=======================================================================
            ///
            ///  INSTACIAS DE REPOSITORY
            /// 
            ///
            services.AddScoped<IClientesRepository, ClientesRepository>();

            ////=======================================================================
            ///
            ///  INSTACIAS DE CONTEXTO
            /// 
            ///
            services.AddDbContext<MySQLContext>(options =>
              options.UseMySql(
                  config["ConnectionVideos"],
                  ServerVersion.AutoDetect(config["ConnectionVideos"])
              ));
            services.AddScoped<MySQLContext>();




        }
    }
}
