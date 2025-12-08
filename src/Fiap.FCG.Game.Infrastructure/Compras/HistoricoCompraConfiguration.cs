using Fiap.FCG.Game.Domain.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.FCG.Game.Infrastructure.Compras
{
    public class HistoricoCompraConfiguration : IEntityTypeConfiguration<HistoricoCompra>
    {
        public void Configure(EntityTypeBuilder<HistoricoCompra> builder)
        {
            builder.ToTable("HistoricoCompras");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.Property(x => x.DataCompra)
                .IsRequired();

            builder
                .HasMany(x => x.Itens)
                .WithOne(i => i.HistoricoCompra)
                .HasForeignKey(i => i.HistoricoCompraId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
