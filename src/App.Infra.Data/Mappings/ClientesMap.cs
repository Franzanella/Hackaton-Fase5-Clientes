using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Mappings
{
    public class ClientesMap : IEntityTypeConfiguration<ClienteBD>
    {
        public ClientesMap()
        {

        }
        public void Configure(EntityTypeBuilder<ClienteBD> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.Password)
              .IsRequired()
              .HasMaxLength(100);

            builder.Property(p => p.DataCadastro)
               .IsRequired();


            builder.Property(p => p.Email)
               .HasMaxLength(50);

            builder.ToTable("Clientes");
        }
    }
}
