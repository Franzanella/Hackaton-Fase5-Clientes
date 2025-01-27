using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext() { } // This one

        public FiapDbContext(DbContextOptions<FiapDbContext> options) : base(options)
        { }
        public DbSet<Usuario> Usuarios { get; set; }


          protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlServer("Server=localhost,1433;Database=HackatonBD;User Id=SA;Password=Pa55w0rd2021;TrustServerCertificate=true");

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>()
                .HaveMaxLength(250);

            configurationBuilder.Properties<decimal>()
                 .HavePrecision(18, 2);

        }




    }






}


